package playn.xna;

import java.io.File;
import java.io.PrintWriter;
import java.util.HashMap;

public class ContentProject extends Project {
	
	public String content;
	
	public ContentProject(String name, File contentDir) {
		super(name);
		guid = "AE956C53-4E72-4DDB-ABC9-761075852D63";
		content = getContentItems(contentDir, "assets");
	}
	
	private String getContentItems(File dir, String prefix) {
		String content = "";
		for (String path : dir.list()) {
			File file = new File(dir, path);
			if (file.isDirectory()) {
				content += getContentItems(file, prefix + File.separator + path);
			} else {
				int lastDot = path.lastIndexOf(".");
				if (lastDot >= 0 && lastDot < path.length()) {
					String ext = path.substring(lastDot);
					if (extMap.containsKey(ext)) {
						ContentType type = extMap.get(ext);
						HashMap<String, String> map = new HashMap<String, String>();
						map.put("path", prefix + File.separator + path);
						map.put("name", path);
						map.put("importer", type.importer);
						map.put("processor", type.processor);
						content += FormatUtils.format(CONTENT_FORAMT, map);						
					}
				}
			}
		}
		return content;
	}

	public void write(PrintWriter writer) {
		HashMap<String, String> map = new HashMap<String, String>();
		map.put("guid", guid);
		map.put("name", name);
		map.put("content", content);
		writer.print(FormatUtils.format(FORMAT, map));
	}

	public final static HashMap<String, ContentType> extMap = new HashMap<String, ContentType>();
	static {
		ContentType texture = new ContentType("TextureImporter", "TextureProcessor");
		extMap.put(".png", texture);
		extMap.put(".bmp", texture);
		extMap.put(".jpg", texture);
		extMap.put(".jpeg", texture);
		ContentType mp3 = new ContentType("Mp3Importer", "SoundEffectProcessor");
		extMap.put(".mp3", mp3);
		ContentType text = new ContentType("TextImporter", "TextProcessor");
		extMap.put(".txt", text);
		extMap.put(".csv", text);
		extMap.put(".json", text);
	}
	
	public static class ContentType {
		public String importer, processor;
		public ContentType(String importer, String processor) {
			this.importer = importer; this.processor = processor;
		}
	}
	
	public final static String FORMAT = 
			"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" + 
			"<Project DefaultTargets=\"Build\" ToolsVersion=\"4.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n" + 
			"  <PropertyGroup>\n" + 
			"    <ProjectGuid>{$guid$}</ProjectGuid>\n" + 
			"    <ProjectTypeGuids>{96E2B04D-8817-42c6-938A-82C39BA4D311};{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}</ProjectTypeGuids>\n" + 
			"    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\n" + 
			"    <Platform Condition=\" '$(Platform)' == '' \">x86</Platform>\n" + 
			"    <OutputType>Library</OutputType>\n" + 
			"    <AppDesignerFolder>Properties</AppDesignerFolder>\n" + 
			"    <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>\n" + 
			"    <XnaFrameworkVersion>v4.0</XnaFrameworkVersion>\n" + 
			"    <OutputPath>bin\\$(Platform)\\$(Configuration)</OutputPath>\n" + 
			"    <ContentRootDirectory>Content</ContentRootDirectory>\n" + 
			"  </PropertyGroup>\n" + 
			"  <PropertyGroup Condition=\"'$(Configuration)|$(Platform)' == 'Debug|x86'\">\n" + 
			"    <PlatformTarget>x86</PlatformTarget>\n" + 
			"  </PropertyGroup>\n" + 
			"  <PropertyGroup Condition=\"'$(Configuration)|$(Platform)' == 'Release|x86'\">\n" + 
			"    <PlatformTarget>x86</PlatformTarget>\n" + 
			"  </PropertyGroup>\n" + 
			"  <PropertyGroup>\n" + 
			"    <RootNamespace>$name$</RootNamespace>\n" + 
			"  </PropertyGroup>\n" + 
			"  <ItemGroup>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Content.Pipeline.EffectImporter, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=MSIL\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Content.Pipeline.FBXImporter, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=MSIL\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Content.Pipeline.TextureImporter, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=MSIL\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Content.Pipeline.XImporter, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=MSIL\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Content.Pipeline.AudioImporters, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=MSIL\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"Microsoft.Xna.Framework.Content.Pipeline.VideoImporters, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553, processorArchitecture=MSIL\">\n" + 
			"      <Private>False</Private>\n" + 
			"    </Reference>\n" + 
			"    <Reference Include=\"../game/PlayNContentImporter\">\n" + 
			"      <HintPath>../game/PlayNContentImporter.dll</HintPath>\n" + 
			"    </Reference>\n" + 
			"  </ItemGroup>\n" + 
			"$content$" + 
			"  <Import Project=\"$(MSBuildExtensionsPath)\\Microsoft\\XNA Game Studio\\$(XnaFrameworkVersion)\\Microsoft.Xna.GameStudio.ContentPipeline.targets\" />\n" + 
			"</Project>\n";
	
	public final static String CONTENT_FORAMT =
			"  <ItemGroup>\n" + 
			"    <Compile Include=\"$path$\">\n" + 
			"      <Name>$name$</Name>\n" + 
			"      <Importer>$importer$</Importer>\n" + 
			"      <Processor>$processor$</Processor>\n" + 
			"    </Compile>\n" + 
			"  </ItemGroup>\n";
}
