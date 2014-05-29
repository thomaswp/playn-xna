# ikvm-maven-plugin

This project is a fork of [https://github.com/samskivert/ikvm-maven-plugin](samskiver's plugin).

This Maven plugin runs IKVM on a collection of Java jar files (defined by the
dependencies in the POM that includes this plugin).

The primary putpose is to generate DLLs for use by the XNA backend of
the [PlayN] cross-platform game development library. This project requires a
compiled .dll of the PlayN core library, which is included in the parent repo
under the compiled folder. Because this is intended for use with XNA, it has
been designed and tested on Windows.

It defines a `dll` packaging type and generates a `dll` artifact.

## Usage

One must configure their IKVM installation location in Maven's global settings
(`~/.m2/settings.xml`). For example:

    <profiles>
      <profile>
        <id>ikvm</id>
        <properties>
		  <!-- Path to your ikvm installation -->
          <ikvm.path>C:\Program Files\ikvm</ikvm.path>
		  <!-- Path to the .NET .dlls, either from a .NET Framework or Mono installation -->
		  <dll.path>C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\Profile\Client\</dll.path>
		  <!-- Path to the folder containing the PlayN-x.x.x.dll file, as well as any other required .dlls for XNA -->
		  <!-- If you have downloaded this repo, this folder should already exist at ../compiled -->
		  <playn.path>C:\...\compiled</playn.path>
        </properties>
      </profile>
    </profiles>
    <activeProfiles>
      <activeProfile>ikvm</activeProfile>
    </activeProfiles>

Once that's done, the following POM fragment demonstrates the use of this plugin:

    <?xml version="1.0" encoding="UTF-8"?>
    <project ...>
      <modelVersion>4.0.0</modelVersion>
      <groupId>foo</groupId>
      <artifactId>bar-ios</artifactId>
      <version>1.0-SNAPSHOT</version>
      <packaging>dll</packaging>

      <dependencies>
        <dependency>
          <groupId>foo</groupId>
          <artifactId>bar-core</artifactId>
          <version>${project.version}</version>
        </dependency>

        <dependency>
          <groupId>baz</groupId>
          <artifactId>bif</artifactId>
          <version>1.2</version>
        </dependency>
      </dependencies>

      <build>
        <plugins>
          <plugin>
			<groupId>playn.xna</groupId>
			<artifactId>ikvm-maven-plugin</artifactId>
			<version>1.3</version>
			<extensions>true</extensions>
			<configuration>
			  <ikvmArgs>
				<ikvmArg>-debug</ikvmArg>
			  </ikvmArgs>
			  <createStub>true</createStub>
			  <dlls>
			  </dlls>
			  <copyDlls>
				<copyDll>bin/IKVM.Runtime.dll</copyDll>
				<copyDll>bin/IKVM.Runtime.JNI.dll</copyDll>
				<copyDll>bin/IKVM.OpenJDK.Core.dll</copyDll>
				<copyDll>bin/IKVM.OpenJDK.Util.dll</copyDll>
				<copyDll>bin/IKVM.OpenJDK.Text.dll</copyDll>
				<!-- PlayN dlls, placed in the "playn.path" folder -->
				<copyDll>PlayN-1.8.5.dll</copyDll>
				<copyDll>PlayNXNA.dll</copyDll>
				<copyDll>PlayNContentImporter.dll</copyDll>
			  </copyDlls>
			</configuration>
		  </plugin>
        </plugins>
      </build>
    </project>


## License

ikvm-maven-plugin is released under the New BSD License, which can be found in
the [LICENSE] file.

[PlayN]: http://code.google.com/p/playn
[LICENSE]: https://github.com/samskivert/ikvm-maven-plugin/blob/master/LICENSE
