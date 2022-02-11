use glib::clone;
use gtk::*;
use gtk::prelude::*;

fn on_activate(application: &Application) {
    let window = ApplicationWindow::builder()
        .application(application)
        .default_height(600)
        .default_width(800)
        .resizable(false)
        .title("EasyEPUBCheck v2.0.0")
        .build();

    let outer_container = Box::builder()
        .orientation(Orientation::Vertical)
        .spacing(6)
        .build();

    let selection_container = Box::builder()
        .orientation(Orientation::Horizontal)
        .spacing(0)
        .build();

    let chosen_epub_text = Entry::builder()
        .hexpand(true)
        .can_focus(false)
        .can_target(false)
        .text("Choose EPUB -->")
        .editable(false)
        .build();

    let choose_epub_button = Button::builder()
        .label("Choose EPUB")
        .margin_end(0)
        .build();

    let validate_button = Button::builder()
        .label("Validate")
        .hexpand(false)
        .halign(Align::Center)
        .sensitive(false)
        .build();

    let output_scroll_container = ScrolledWindow::builder().vexpand(true).build();

    let output_textbox = TextView::builder()
        .vexpand(true)
        .margin_start(12)
        .margin_end(12)
        .editable(false)
        .build();

    let action_buttons_container = Box::builder()
        .orientation(Orientation::Horizontal)
        .margin_start(12)
        .margin_end(12)
        .margin_bottom(12)
        .build();

    let save_file_button = Button::builder()
        .label("Save Output to File")
        .sensitive(false)
        .halign(Align::Start)
        .build();

    let spacer = Box::builder()
        .hexpand(true)
        .build();

    let close_button = Button::builder()
        .label("Close")
        .halign(Align::Start)
        .build();

    let open_file_dialog = FileChooserDialog::new(
        Some("Open File"),
        Some(&window),
        FileChooserAction::Open,
        &[("Open", ResponseType::Ok), ("Cancel", ResponseType::Cancel)]
    );

    open_file_dialog.connect_response(clone!(@weak chosen_epub_text => move |fcd, response| {
        if response == ResponseType::Ok {
            let filename = fcd.file().expect("").path().expect("Couldn't get filename").display().to_string();
            chosen_epub_text.set_text(&filename);
        }
        fcd.close();
    }));

    let save_file_dialog = FileChooserDialog::new(
        Some("Open File"),
        Some(&window),
        FileChooserAction::Save,
        &[("Save", ResponseType::Ok), ("Cancel", ResponseType::Cancel)]
    );
    save_file_dialog.set_decorated(true);

    save_file_dialog.connect_response(clone!(@weak output_textbox => move |fcd, response| {
        if response == ResponseType::Ok {
            let filename = fcd.file().expect("").path().expect("Couldn't get filename").display().to_string();
            let buffer = output_textbox.buffer();
            let content = buffer.text(&buffer.start_iter(), &buffer.end_iter(), false).to_string();
            std::fs::write(filename, content).expect("Could not write to file");
        }
        fcd.close();
    }));

    close_button.connect_clicked(clone!(@weak window => move |_| window.close()));

    choose_epub_button.connect_clicked(clone!(@weak validate_button => move |_| {
        open_file_dialog.show();
        validate_button.set_sensitive(true);
    }));

    validate_button.connect_clicked(clone!(@weak output_textbox, @weak chosen_epub_text, @weak save_file_button => move |_| {
        output_textbox.buffer().set_text("Validating; please wait...");
        output_textbox.buffer().set_text(call_epubcheck(&chosen_epub_text.text()).as_str());
        save_file_button.set_sensitive(true);
    }));

    save_file_button.connect_clicked(move |_| save_file_dialog.show());

    selection_container.append(&chosen_epub_text);
    selection_container.append(&choose_epub_button);
    outer_container.append(&selection_container);

    outer_container.append(&validate_button);

    output_scroll_container.set_child(Some(&output_textbox));
    outer_container.append(&output_scroll_container);

    action_buttons_container.append(&save_file_button);
    action_buttons_container.append(&spacer);
    action_buttons_container.append(&close_button);
    outer_container.append(&action_buttons_container);

    window.set_child(Some(&outer_container));
    window.present();
}

fn call_epubcheck(path: &str) -> String {
    let output = std::process::Command::new("w3c/epubcheck").arg(path).output().expect("Failed to launch epubcheck");
    let stderr = String::from_utf8_lossy(&output.stderr).into_owned();
    let stdout = String::from_utf8_lossy(&output.stdout).into_owned();
    if !stderr.is_empty() {
        stderr
    } else if !stdout.is_empty() {
        stdout
    } else {
        "No output from epubcheck".to_string()
    }
}

fn main() {
    let app = Application::builder()
        .application_id("com.hnrhn.EasyEPUBCheck")
        .build();
    app.connect_activate(on_activate);
    app.run();
}
