package com.hnrhn.epubcheckgui;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

public class App extends Application {
    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage stage) throws Exception {
        stage.setTitle("EpubCheckGUI");

        Parent root = FXMLLoader.load(getClass().getResource("/scene.fxml"));

        stage.setScene(new Scene(root));
        stage.show();
    }
}
