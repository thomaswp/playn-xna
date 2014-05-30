package playn.xna;

public abstract class Project implements IWritable {
	public String guid;
	public String name;
	
	public Project(String name) {
		this.name = name;
	}
}
