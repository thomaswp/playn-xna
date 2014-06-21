PlayN-XNA
=========

This project is an XNA backend for the [PlayN](https://github.com/threerings/playn) library. The project is under development and includes most of the core functionality of PlayN. The project has been tested for Windows XNA games, but support for the Xbox 360 and other platforms through [monogame](http://www.monogame.net/) are intended for future releases.

Requirements
------------
Before getting started, make sure that you have the following programs installed and can create XNA games on your system:
* [Visual Studio Express 2010](http://www.visualstudio.com/en-us/downloads#d-2010-express)
* [The XNA 4.0 Framework](http://www.microsoft.com/en-us/download/details.aspx?id=23714) (If using Windows 8, you will first need to download the [Windows Marketplace Game Client](http://www.xbox.com/en-US/LIVE/PC/DownloadClient).)

Quick Setup
-----------

For a quick demonstration of PlayN-XNA, clone this project and open PlayNXNA.sln. Most of the PlayN sample projects are included as XNA games. Choose a project and run it to see the XNA implementation.

Using PlayN-XNA
---------------

To use the platform with your own XNA project, you will need to set a few things up first. First, follow the setup instructions for the [PlayN-XNA samples](https://github.com/thomaswp/playn-xna-samples#setup), making sure you download the additional dependencies for building a maven project. This project also serves as an example from which to work setting up your own project. You may wish to build one of the sample projects to make sure everything is in place before proceeding.

To add an XNA target to your PlayN project, add the following profile to your primary pom.xml:

    <profile>
      <id>xna</id>
      <modules><module>xna</module></modules>
    </profile>
    
Then create a folder called xna and copy the [sample-xna-pom.xml](/sample-xna-pom.xml) file into the directory and rename it pom.xml. Go through and replace the all capitalized portions with values appropriate to your project. Customize the plugin parameters to your needs.

Finally, run the following command from your projects root directory to build:

    mvn -Pxna package
    
This will create a .sln file in your xna folder. With any luck, that project will contain a complete build of your project on the XNA platform. Changes you make to this code will not be overwritten unless requested in the playn-xna-plugin parameters. By default, only the .contentproject will be overwritten (to update when new resources are available).


FAQ
---
