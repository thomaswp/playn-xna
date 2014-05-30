//
// ikvm-maven-plugin - generates C# DLL from Java code via IKVM
// http://github.com/samskivert/ikvm-maven-plugin/blob/master/LICENSE

package playn.xna;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Scanner;

import org.apache.maven.plugin.AbstractMojo;
import org.apache.maven.plugin.MojoExecutionException;
import org.apache.maven.project.MavenProject;

/**
 * @requiresDependencyResolution compile
 * @goal xna
 * @phase package
 */
public class XnaPluginMojo extends AbstractMojo
{
	
	/**
     * The fully qualified name of the core game that should be run,
     * e.g. my.package.core.MyGame 
     * @parameter expression="${qualifiedGameName}"
     */
    public String qualifiedGameName;
    
    /**
     * Any spritefonts the plugin should generate.
     * Fonts should be in the form: FontName;InstalledName;Sizes;Styles;CharacterRegions
     * Example: <code>&lt;spritefont>Arial;Arial;9,12,24;Plain,Bold,Italic;32-136,150-170&lt;/spritefont></code>
     * @parameter expression="${spritefonts}"
     */
    public String[] spritefonts;
    
    public boolean overwriteSLN = false;
    public boolean overwriteProject = false;
    public boolean overwriteContent = true;
    public boolean overwriteGame = false;
    public boolean overwriteImporter = true;
	
    public void execute() throws MojoExecutionException {
    	
    	String finalName = _project.getBuild().getFinalName();
    	String[] parts = finalName.split("\\-");
    	String gameName = "";
    	for (String part : parts) {
    		if (part.equalsIgnoreCase("xna")) continue;
    		gameName += part.substring(0, 1).toUpperCase() + part.substring(1).toLowerCase();
    	}
    	String assets = _project.getBasedir() + File.separator + "content" + File.separator + "assets";
    	
    	String slnPath = _project.getBasedir() + File.separator + gameName + ".sln";
    	String projectPath = _project.getBasedir() + File.separator + "game" + File.separator + gameName + ".csproj";
    	String contentPath = _project.getBasedir() + File.separator + "content" + File.separator + gameName + "Content.contentproj";
    	String gamePath = _project.getBasedir() + File.separator + "game" + File.separator + gameName + "XNA.cs";
    	String importerPath = _project.getBasedir() + File.separator + "game" + File.separator + "Importer.cs";
    	String fontsPath = _project.getBasedir() + File.separator + "content" + File.separator + "fonts";
    	
    	getLog().debug("Game name: " + gameName);
    	getLog().debug("Qualified Game name: " + qualifiedGameName);
    	getLog().debug("Library name: " + finalName);
    	getLog().debug("Assets path: " + assets);
    	
    	Font[] fonts = new Font[0];
    	if (this.spritefonts != null) fonts = Font.parseFonts(this.spritefonts);
    	File fontPathFile = new File(fontsPath); 
    	if (!fontPathFile.exists()) {
    		fontPathFile.mkdir();
    	}
    	for (Font font : fonts) {
    		write(font, fontsPath + File.separator + font.getFilename(true), true);
    	}
    	
    	XNAProject project = new XNAProject(gameName, finalName, new File(assets), fontPathFile);
    	Solution solution = new Solution(project);
    	Game game = new Game(qualifiedGameName, project);
    	Importer importer = new Importer(project, fonts);
    	
    	write(project, projectPath, overwriteProject);
    	write(project.content, contentPath, overwriteContent);
    	write(solution, slnPath, overwriteSLN);
    	write(importer, importerPath, overwriteImporter);
    	
    	if (qualifiedGameName != null) {
        	write(game, gamePath, overwriteGame);
    	} else {
    		getLog().warn("Please provide a qualifiedGameName configuration property");
    	}
    }

	private void write(IWritable item, String path, boolean overwrite) {
		try {
			File file = new File(path);
			if (!overwrite && file.exists()) return;
	    	PrintWriter slnWriter = new PrintWriter(new FileWriter(file));
	    	item.write(slnWriter);
	    	slnWriter.close();
    	} catch (IOException e) {
    		getLog().info("Failed to write " + path);
    		getLog().debug(e);
    	}
	}
    
    public static void main(String[] args) {
    	String[] fonts = new String[] {
    			"Arial;Arial;9,12,24;Plain,Bold;32-136,200-300"
    	};
    	Font[] fs = Font.parseFonts(fonts);
    	PrintWriter pw = new PrintWriter(System.out);
    	for (Font font : fs) 
        	font.write(pw);
    	pw.flush();
//    	writeJavaString("C:\\Users\\Thomas\\Documents\\Visual Studio 2010\\Projects\\PlayNXNA\\Showcase\\ShowcaseContent\\Helvetica-9.spritefont");
    }
    
    public static void writeJavaString(String path) {
    	try {
    		Scanner sc = new Scanner(new FileInputStream(path));
    		while (sc.hasNext()) {
    			String line = sc.nextLine();
    			line = line.replace("\\", "\\\\");
    			line = line.replace("\"", "\\\"");
    			line = line.replace("\t", "\\t");
    			line = "\"" + line + "\\n\"";
    			if (sc.hasNext()) line += " + ";
    			System.out.println(line);
    		}
    		sc.close();
    	} catch (Exception e) {
    		e.printStackTrace();
    	}
    }
    
    /** @parameter default-value="${project}" */
    private MavenProject _project;
}
