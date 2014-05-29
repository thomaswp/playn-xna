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
    	String gamePath = _project.getBasedir() + File.separator + "game" + File.separator + gameName + ".csproj";
    	String contentPath = _project.getBasedir() + File.separator + "content" + File.separator + gameName + "Content.contentproj";
    	
    	getLog().debug("Game name: " + gameName);
    	getLog().debug("Library name: " + finalName);
    	getLog().debug("Assets path: " + assets);
    	getLog().debug("SLN path: " + slnPath);
    	getLog().debug("Game path: " + gamePath);
    	getLog().debug("Content path: " + contentPath);
    	
    	XNAProject game = new XNAProject(gameName, finalName, new File(assets));
    	Solution solution = new Solution(game);

    	
		try {
			PrintWriter gameWriter = new PrintWriter(new FileWriter(gamePath));
	    	game.write(gameWriter);
	    	gameWriter.close();
		} catch (IOException e) {
    		getLog().info("Failed to write csproj");
    		getLog().debug(e);
		}
    	
		try {
	    	PrintWriter contentWriter = new PrintWriter(new FileWriter(contentPath));
	    	game.content.write(contentWriter);
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
    	
    	
    	
    	
    }
    
    public static void writeJavaString(String path) {
    	try {
    		Scanner sc = new Scanner(new FileInputStream(path));
    		while (sc.hasNext()) {
    			String line = sc.nextLine();
    			line = line.replaceAll("\\\\", "\\\\\\\\");
    			line = line.replaceAll("\"", "\\\\\"");
    			line = line.replaceAll("\t", "\\\\t");
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
