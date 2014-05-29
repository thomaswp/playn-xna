package playn.xna;

import java.util.HashMap;

public class FormatUtils {
	public static String format(String format, HashMap<String, String> map) {
		for (String key : map.keySet()) {
			format = format.replace("$" + key + "$", map.get(key));
		}
		return format;
	}
}
