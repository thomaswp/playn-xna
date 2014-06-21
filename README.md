PlayN-XNA
=========

This project is an XNA backend for the [PlayN](https://github.com/threerings/playn) library. The project is under development and includes most of the core functionality of PlayN. The project has been tested for Windows XNA games, but support for the Xbox 360 and other platforms through [monogame](http://www.monogame.net/) are intended for future releases.

This project contains the source files for PlayN-XNA, as well as easy-to-run test projects, but to use this in you own project, you will only need the [PlayN 1.8.5 dlls](https://github.com/thomaswp/playn-xna/raw/master/compiled/PlayN-1.8.5-dlls.zip).

Requirements
------------
Before getting started, make sure that you have the following programs installed and can create XNA games on your system:
* [Visual Studio Express 2010](http://www.visualstudio.com/en-us/downloads#d-2010-express)
* [The XNA 4.0 Framework](http://www.microsoft.com/en-us/download/details.aspx?id=23714) (If using Windows 8, you will first need to download the [Windows Marketplace Game Client](http://www.xbox.com/en-US/LIVE/PC/DownloadClient).)

Quick Setup
-----------

For a quick demonstration of PlayN-XNA, clone this project and open PlayNXNA.sln. Most of the PlayN sample projects are included as XNA games. Choose a project and run it to see the XNA implementation.

Using PlayN-XNA in Your Own Project
-----------------------------------

To use the platform with your own XNA project, you will need to set a few things up first. First, follow the setup instructions for the [PlayN-XNA samples](https://github.com/thomaswp/playn-xna-samples#setup), making sure you download the additional dependencies for building a maven project. This project also serves as an example from which to work setting up your own project. You may wish to build one of the sample projects to make sure everything is in place before proceeding.

To add an XNA target to your PlayN project, add the following profile to your primary pom.xml:

    <profile>
      <id>xna</id>
      <modules><module>xna</module></modules>
    </profile>
    
Then create a folder called xna and copy the [sample-xna-pom.xml](/sample-xna-pom.xml) file into the directory and rename it pom.xml. Go through and replace the all-caps items with values appropriate to your project (your iOS pom.xml might be a good reference). Customize the plugin parameters to your needs.

Finally, run the following command from your project's root directory to build:

    mvn -Pxna package
    
This will create a .sln file in your xna folder. With any luck, that project will contain a complete build of your project for the XNA platform. When your core code has updated, you can run this command again to regenerate your dlls. Changes you make to C# code inside the xna folder will not be overwritten unless requested in the playn-xna-plugin parameters. By default, only the .dll's and .contentproject will be overwritten (to update when new resources are available).

Structure
---------

The PlayNXNA solution contains a number of projects. If you are interested in running the samples of contributing to the platform, this might be useful:
* PlayNXNA contains the XNA backend to the PlayN framework. Like all PlayN backends, it is a library to be referenced by each game's executable. Test projects should reference this library as a project, and include the PlayN-1.8.5.dll and IKVM.OpenJDK.Core.dll files as well.
* PlayNXNAContentImporter contains XNA content importers and processors to handle assets not recognized by the XNA framework, such as text and json files. ContentProjects should reference this project, which allows you to import these assets.
* HelloGame/Content contains a build of the 'hello' PlayN sample for quick testing when the backend is updated.
* CuteGame/Content contains a build of the 'cute' PlayN sample for quick testing when the backend is updated.
* ShowcaseGame/Content contains a build of the 'showcase' PlayN sample for quick testing when the backend is updated.
* TuxBlockGame/Content contains a build of the [TuxBlocks](https://github.com/thomaswp/tuxblocks) game, which is a completed, open source game with many more required features than the PlayN samples. The backend currently does not implement all the required features of the game.
* TestGame/Content contains a PlayN game written in C# for testing specific features, namely canvas functionality.

FAQ
---
