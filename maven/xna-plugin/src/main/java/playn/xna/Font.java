package playn.xna;

import java.io.PrintWriter;
import java.util.HashMap;
import java.util.List;
import java.util.ArrayList;

public class Font implements IWritable {
	
	private final static String[] STYLES_XNA = new String[] {
		"Regular", "Bold", "Italic", "Bold, Italic"
	};
	
	private final static String[] STYLES_FILE = new String[] {
		"Plain", "Bold", "Italic", "BoldItalic"
	};
	
	private final static String[] STYLES_PLAYN = new String[] {
		"PLAIN", "BOLD", "ITALIC", "BOLD_ITALIC"
	};
	
	public String name, file;
	public float size;
	public boolean bold, italic;
	public int[] regions;
	
	
	
	public String getFilename(boolean extension) {		
		return String.format("%s-%.01f-%s%s", name, size, STYLES_FILE[getStyleIndex()],
				extension ? ".spritefont" : "");
	}
	
	private int getStyleIndex() {
		int index = 0;
		if (bold) index++;
		if (italic) index += 2;
		return index;
	}
	public String getPlayNStyle() {
		return STYLES_PLAYN[getStyleIndex()];
	}
	
	public void write(PrintWriter writer) {
		HashMap<String, String> map = new HashMap<String, String>();
		map.put("file", file);
		map.put("size", String.format("%.02f", size));
		map.put("style", STYLES_XNA[getStyleIndex()]);
		map.put("regions", getRegions());
		
		writer.print(FormatUtils.format(FORMAT, map));
	}
	
	private String getRegions() {
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < regions.length; i += 2) {
			int start = regions[i];
			int end = regions[i + 1];
			HashMap<String, String> map = new HashMap<String, String>();
			map.put("start", "" + start);
			map.put("end", "" + end);
			sb.append(FormatUtils.format(REGION_FORMAT, map));
		}
		return sb.toString();
	}
	
	//Arial;Arial;9,12,24;Plain,Bold,Italic;32-136,150-170
	public static Font[] parseFonts(String[] fontStrings) {
		List<Font> fonts = new ArrayList<Font>();
		
		for (String fontString : fontStrings) {
			String[] parts = fontString.split(";");
			
			String[] sizeStrings = parts[2].split(",");
			float sizes[] = new float[sizeStrings.length];
			for (int i = 0; i < sizes.length; i++) {
				sizes[i] = Float.parseFloat(sizeStrings[i]);
			}
			
			boolean[] bold = new boolean[] { true, false };
			boolean[] italic = new boolean[] { true, false };
			if (parts.length > 3 && parts[3].length() > 0) {
				String[] stylesStrings = parts[3].split(",");
				bold[0] = italic[0] = false;
				for (String style : stylesStrings) {
					if (style.equalsIgnoreCase("plain")) {
						bold[0] = italic[0] = true;
					} else if (style.equalsIgnoreCase("bold")) {
						bold[1] = true;
					} else if (style.equalsIgnoreCase("italic")) {
						italic[1] = true;
					}
				}
			}
			
			ArrayList<Integer> regionsList = new ArrayList<Integer>();
			regionsList.add(32); regionsList.add(136);
			if (parts.length > 4 && parts[4].length() > 0) {
				regionsList.clear();
				String[] regionStrings = parts[4].split(",");
				for (String region : regionStrings) {
					String[] ends = region.split("-");
					int start = Integer.parseInt(ends[0]);
					int end = Integer.parseInt(ends[1]);
					regionsList.add(start);
					regionsList.add(end);
				}
			}
			int[] regions = new int[regionsList.size()];
			for (int i = 0; i < regions.length; i++) regions[i] = regionsList.get(i);
			
			for (float size : sizes) {
				for (int bi = 0; bi < 2; bi++) {
					for (int ii = 0; ii < 2; ii++) {
						if (!bold[bi] || !italic[ii]) continue;
						
						Font font = new Font();
						fonts.add(font);
						
						font.name = parts[0];
						font.file = parts[1];
						font.size = size;
						font.bold = bi == 1;
						font.italic = ii == 1;
						font.regions = regions;
					}
				}
			}
		}
		
		return fonts.toArray(new Font[fonts.size()]);
	}
	
	public final static String FORMAT = 
			"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" + 
			"<XnaContent xmlns:Graphics=\"Microsoft.Xna.Framework.Content.Pipeline.Graphics\">\n" + 
			"  <Asset Type=\"Graphics:FontDescription\">\n" + 
			"    <FontName>$file$</FontName>\n" + 
			"    <Size>$size$</Size>\n" + 
			"    <Spacing>0</Spacing>\n" + 
			"    <UseKerning>true</UseKerning>\n" + 
			"    <Style>$style$</Style>\n" + 
			"    <CharacterRegions>\n" + 
			"$regions$" +
			"    </CharacterRegions>\n" + 
			"  </Asset>\n" + 
			"</XnaContent>\n";
	
	public final static String REGION_FORMAT =
			"      <CharacterRegion>\n" + 
			"        <Start>&#$start$;</Start>\n" + 
			"        <End>&#$end$;</End>\n" + 
			"      </CharacterRegion>\n";



}
