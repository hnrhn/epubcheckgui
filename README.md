# EPUBCheckGUI

The W3C's EPUBCheck tool is a command-line program that validates the contents of EPUB files.

EPUBCheckGUI takes that tool and wraps it in a pretty window with shiny buttons, for the benefit of anyone who isn't too confident with the command line:

![A screenshot of the EPUBCheckGUI main window](/screenshot.jpg?raw=true)

## Requirements
Java 13 or higher. Can be acquired from [AdoptOpenJDK](https://www.adoptopenjdk.net) if needed.

## How to use
1. Download the JAR file from my [latest release](https://github.com/hnrhn/epubcheckgui/releases/latest).
2. On most systems with Java correctly installed, double-clicking the JAR file will launch EPUBCheckGUI.
    - If not, you may need to look up how to run a .jar file on your system
3. Choose an EPUB file by clicking the "Choose EPUB" button and browsing for your file.
4. Click "Validate". EPUBCheckGUI will process your file and display your result.
5. If you want to save the result to refer to again later, click the "Save result to file" button and choose a location and name for your report file.

## Roadmap
- Drop required Java version to 11 (or maybe 8) for better compatibility
- Create platform-specific installers
    - with embedded JRE
    - without embedded JRE