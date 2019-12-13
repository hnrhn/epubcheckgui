package com.hnrhn.epubcheckgui;

import com.adobe.epubcheck.api.EpubCheck;
import javafx.application.Platform;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.stage.FileChooser;

import java.io.*;
import java.nio.charset.StandardCharsets;

public class FxmlController {
    private File epubFile;
    private File outputFile;

    @FXML
    private Button chooseButton;

    @FXML
    private Button validateButton;

    @FXML
    private Button closeButton;

    @FXML
    private Button saveToFileButton;

    @FXML
    private TextField filePathField;

    @FXML
    private TextArea resultsField;

    @FXML
    private ProgressIndicator loadingCircle;

    @FXML
    private void chooseFile(ActionEvent event) {
        FileChooser fc = new FileChooser();
        FileChooser.ExtensionFilter epubFilter = new FileChooser.ExtensionFilter("EPUB Files", "*.epub", "*.odf");
        fc.getExtensionFilters().add(epubFilter);
        if (epubFile != null) {
            fc.setInitialDirectory(epubFile.getParentFile());
        }
        File chosenFile = fc.showOpenDialog(chooseButton.getScene().getWindow());
        if (chosenFile != null) {
            epubFile = chosenFile;
            filePathField.setText(chosenFile.getAbsolutePath());
            validateButton.setDisable(false);
        }
    }

    @FXML
    private void checkEpub(ActionEvent event) {
        // Backup the default error stream
        PrintStream errorSteamBackup = System.err;

        // Set up a new error stream to capture stderr messages, which is where epubCheck will print Warnings and Errors
        final ByteArrayOutputStream baos = new ByteArrayOutputStream();
        PrintStream errStream = new PrintStream(baos, true, StandardCharsets.UTF_8);
        System.setErr(errStream);

        // Perform the epub validation in the background
        Thread backgroundThread = new Thread(() -> {
            // Perform the validation
            EpubCheck epubCheck = new EpubCheck(epubFile);
            epubCheck.validate();
            System.setErr(errorSteamBackup);

            String errorsAndWarnings = new String(baos.toByteArray(), StandardCharsets.UTF_8);

            if (errorsAndWarnings.equals("")) {
                resultsField.setText("No errors found!");
                saveToFileButton.setDisable(true);  // In case of sequential checks
            }
            else {
                resultsField.setText(errorsAndWarnings);
                saveToFileButton.setDisable(false);
            }

            loadingCircle.setVisible(false);
        });

        loadingCircle.setVisible(true);
        backgroundThread.start();   // TODO: Allow canceling this thread without closing program.
    }

    @FXML
    private void saveErrorsToFile(ActionEvent event) {
        FileChooser fc = new FileChooser();
        FileChooser.ExtensionFilter txtFilter = new FileChooser.ExtensionFilter("TXT Files", "*.txt");
        fc.getExtensionFilters().add(txtFilter);
        fc.setSelectedExtensionFilter(txtFilter);
        if (outputFile != null) {
            fc.setInitialDirectory(outputFile.getParentFile());
        }

        File saveFile = fc.showSaveDialog(saveToFileButton.getScene().getWindow());
        if (saveFile != null) {
            try {
                FileWriter fw = new FileWriter(saveFile);
                fw.write(resultsField.getText());
                fw.close();
            } catch (IOException e) {
                Alert alert = new Alert(Alert.AlertType.ERROR, "Saving output failed");
                alert.show();
            }
        }
    }

    @FXML
    private void close(ActionEvent event) {
        Platform.exit();
    }
}
