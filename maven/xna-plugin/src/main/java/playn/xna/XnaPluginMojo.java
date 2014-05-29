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
    	
    	getLog().debug("Game name: " + gameName);
    	getLog().debug("Qualified Game name: " + qualifiedGameName);
    	getLog().debug("Library name: " + finalName);
    	getLog().debug("Assets path: " + assets);
    	getLog().debug("SLN path: " + slnPath);
    	getLog().debug("Project path: " + projectPath);
    	getLog().debug("Content path: " + contentPath);
    	getLog().debug("Game path: " + gamePath);
    	
    	XNAProject project = new XNAProject(gameName, finalName, new File(assets));
    	Solution solution = new Solution(project);
    	Game game = new Game(qualifiedGameName, project);
    	
		try {
			PrintWriter projectWriter = new PrintWriter(new FileWriter(projectPath));
	    	project.write(projectWriter);
	    	projectWriter.close();
		} catch (IOException e) {
    		getLog().info("Failed to write csproj");
    		getLog().debug(e);
		}
    	
		try {
	    	PrintWriter contentWriter = new PrintWriter(new FileWriter(contentPath));
	    	project.content.write(contentWriter);
	    	contentWriter.close();
		} catch (IOException e) {
    		getLog().info("Failed to write contentproj");
    		getLog().debug(e);
		}
    	
    	try {
	    	PrintWriter slnWriter = new PrintWriter(new FileWriter(slnPath));
	    	solution.write(slnWriter);
	    	slnWriter.close();
    	} catch (IOException e) {
    		getLog().info("Failed to write solution");
    		getLog().debug(e);
    	}
    	
    	if (qualifiedGameName != null) {
	    	try {
		    	PrintWriter gameWriter = new PrintWriter(new FileWriter(gamePath));
		    	game.write(gameWriter);
		    	gameWriter.close();
	    	} catch (IOException e) {
	    		getLog().info("Failed to write solution");
	    		getLog().debug(e);
	    	}
    	} else {
    		getLog().warn("Please provide a qualifiedGameName configuration property");
    	}
    }
    
    public static void main(String[] args) {
    	writeJavaString("C:\\Users\\Thomas\\Documents\\Eclipse\\Tux\\playn-samples\\hello\\xna\\game\\PlaynHelloXNA.cs");
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
