package playn.xna;

import java.io.File;
import java.io.PrintWriter;
import java.util.HashMap;

public class XNAProject extends Project {
	
	public ContentProject content;
	public String dll;
	
	public XNAProject(String name, String dll, File contentDir) {
		super(name);
		this.dll = dll;
		guid = "A1FAA6C5-27EF-44C3-818C-B82F423C30A9";
		content = new ContentProject(name + "Content", contentDir);
	}
	
	public void write(PrintWriter writer) {
		HashMap<String, String> map = new HashMap<String, String>();
		map.put("name", name);
		map.put("guid", guid);
		map.put("contentGuid", content.guid);
		map.put("dll", dll);
		
		writer.print(FormatUtils.format(FORMAT, map));
	}
	
	String FORMAT = 
			"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" + 
			"<Project DefaultTargets=\"Build\" ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n" + 
			"  <PropertyGroup>\n" + 
			"    <ProjectGuid>{$guid$}</ProjectGuid>\n" + 
			"    <ProjectTypeGuids>{6D335F3A-9D43-41b4-9D22-F6F17C4BE596};{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}</ProjectTypeGuids>\n" + 
			"    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\n" + 
			"    <Platform Condition=\" '$(Platform)' == '' \">x86</Platform>\n" + 
			"    <OutputType>WinExe</OutputType>\n" + 
			"    <AppDesignerFolder>Properties</AppDesignerFolder>\n" + 
			"    <RootNamespace>$name$</RootNamespace>\n" + 
			"    <AssemblyName>$name$</AssemblyName>\n" + 
			"    <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>\n" + 
			"    <TargetFrameworkProfile>Client</TargetFrameworkProfile>\n" + 
			"    <XnaFrameworkVersion>v4.0</XnaFrameworkVersion>\n" + 
			"    <XnaPlatform>Windows</XnaPlatform>\n" + 
			"    <XnaProfile>HiDef</XnaProfile>\n" + 
			"    <XnaCrossPlatformGroupID>d9ff0bb3-c687-4e43-86fd-a30ec883e2ef</XnaCrossPlatformGroupID>\n" + 
			"    <XnaOutputType>Game</XnaOutputType>\n" + 
			"    <PublishUrl>publish\\</PublishUrl>\n" + 
			"    <Install>true</Install>\n" + 
			"    <InstallFrom>Disk</InstallFrom>\n" + 
			"    <UpdateEnabled>false</UpdateEnabled>\n" + 
			"    <UpdateMode>Foreground</UpdateMode>\n" + 
			"    <UpdateInterval>7</UpdateInterval>\n" + 
			"    <UpdateIntervalUnits>Days</UpdateIntervalUnits>\n" + 
			"    <UpdatePeriodically>false</UpdatePeriodically>\n" + 
			"    <UpdateRequired>false</UpdateRequired>\n" + 
			"    <MapFileExtensions>true</MapFileExtensions>\n" + 
			"    <ApplicationRevision>0</ApplicationRevision>\n" + 
			"    <ApplicationVersion>1.0.0.%2a</ApplicationVersion>\n" + 
			"    <IsWebBootstrapper>false</IsWebBootstrapper>\n" + 
			"    <UseApplicationTrust>false</UseApplicationTrust>\n" + 
			"    <BootstrapperEnabled>true</BootstrapperEnabled>\n" + 
			"  </PropertyGroup>\n" + 
			"  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|x86' \">\n" + 
			"    <DebugSymbols>true</DebugSymbols>\n" + 
			"    <DebugType>full</DebugType>\n" + 
			"    <Optimize>false</Optimize>\n" + 
			"    <OutputPath>bin\\x86\\Debug</OutputPath>\n" + 
			"    <DefineConstants>DEBUG;TRACE;WINDOWS</DefineConstants>\n" + 
			"    <ErrorReport>prompt</ErrorReport>\n" + 
			"    <WarningLevel>4</WarningLevel>\n" + 
			"    <NoStdLib>true</NoStdLib>\n" + 
			"    <UseVSHostingProcess>false</UseVSHostingProcess>\n" + 
			"    <PlatformTarget>x86</PlatformTarget>\n" + 
			"    <XnaCompressContent>false</XnaCompressContent>\n" + 
			"  </PropertyGroup>\n" + 
			"  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|x86' \">\n" + 
			"    <DebugType>pdbonly</DebugType>\n" + 
			"    <Optimize>true</Optimize>\n" + 
			"    <OutputPath>bin\\x86\\Release</OutputPath>\n" + 
			"    <DefineConstants>TRACE;WINDOWS</DefineConstants>\n" + 
			"    <ErrorReport>prompt</ErrorReport>\n" + 
			"    <WarningLevel>4</WarningLevel>\n" + 
			"    <NoStdLib>true</NoStdLib>\n" + 
			"    <UseVSHostingProcess>false</UseVSHostingProcess>\n" + 
			"    <PlatformTarget>x86</PlatformTarget>\n" + 
			"    <XnaCompressContent>true</XnaCompressContent>\n" + 
			"  </PropertyGroup>\n" + 
			"  <ItemGroup>\n" + 
			"    <Reference Include=\"IKVM.OpenJDK.Core\">\n" + 
			"      <HintPath>IKVM.OpenJDK.Core.dll</HintPath>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=x86\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Game, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=x86\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Graphics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=x86\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.GamerServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=x86\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Xact, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=x86\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Video, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=x86\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Avatar, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=x86\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Net, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=x86\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Storage, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=x86\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"mscorlib\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"PlayN-1.8.5\">\n" + 
			"      <HintPath>PlayN-1.8.5.dll</HintPath>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"PlayNXNA\">\n" + 
			"      <HintPath>PlayNXNA.dll</HintPath>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"$dll$, Version=0.0.0.0, Culture=neutral, processorArchitecture=MSIL\">\n" + 
			"      <SpecificVersion>False</SpecificVersion>\n" + 
			"      <HintPath>.\\$dll$.dll</HintPath>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"System\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"System.Xml\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"System.Core\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"System.Xml.Linq\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"System.Net\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"  </ItemGroup>\n" + 
			"  <ItemGroup>\n" + 
			"    <Compile Include=\"$name$XNA.cs\" />\n" + 
			"  </ItemGroup>\n" + 
			"  <ItemGroup>\n" + 
			"    <ProjectReference Include=\"..\\content\\$name$Content.contentproj\">\n" + 
			"      <Name>$name$Content %28Content%29</Name>\n" + 
			"      <XnaReferenceType>Content</XnaReferenceType>\n" + 
			"      <Project>{$contentGuid$}</Project>\n" + 
			"    </ProjectReference>\n" + 
			"  </ItemGroup>\n" + 
			"  <ItemGroup>\n" + 
			"    <BootstrapperPackage Include=\".NETFramework,Version=v4.0,Profile=Client\">\n" + 
			"      <Visible>False</Visible>\n" + 
			"      <ProductName>Microsoft .NET Framework 4 Client Profile %28x86 and x64%29</ProductName>\n" + 
			"      <Install>true</Install>\n" + 
			"    </BootstrapperPackage>\n" + 
			"    <BootstrapperPackage Include=\"Microsoft.Net.Client.3.5\">\n" + 
			"      <Visible>False</Visible>\n" + 
			"      <ProductName>.NET Framework 3.5 SP1 Client Profile</ProductName>\n" + 
			"      <Install>false</Install>\n" + 
			"    </BootstrapperPackage>\n" + 
			"    <BootstrapperPackage Include=\"Microsoft.Net.Framework.3.5.SP1\">\n" + 
			"      <Visible>False</Visible>\n" + 
			"      <ProductName>.NET Framework 3.5 SP1</ProductName>\n" + 
			"      <Install>false</Install>\n" + 
			"    </BootstrapperPackage>\n" + 
			"    <BootstrapperPackage Include=\"Microsoft.Windows.Installer.3.1\">\n" + 
			"      <Visible>False</Visible>\n" + 
			"      <ProductName>Windows Installer 3.1</ProductName>\n" + 
			"      <Install>true</Install>\n" + 
			"    </BootstrapperPackage>\n" + 
			"    <BootstrapperPackage Include=\"Microsoft.Xna.Framework.4.0\">\n" + 
			"      <Visible>False</Visible>\n" + 
			"      <ProductName>Microsoft XNA Framework Redistributable 4.0</ProductName>\n" + 
			"      <Install>true</Install>\n" + 
			"    </BootstrapperPackage>\n" + 
			"  </ItemGroup>\n" + 
			"  <Import Project=\"$(MSBuildBinPath)\\Microsoft.CSharp.targets\" />\n" + 
			"  <Import Project=\"$(MSBuildExtensionsPath)\\Microsoft\\XNA Game Studio\\Microsoft.Xna.GameStudio.targets\" />\n" + 
			"</Project>\n";
}
