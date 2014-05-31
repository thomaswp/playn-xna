using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using pythagoras.f;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    public class XNATextLayout : AbstractTextLayout
    {

        public XNATextLayout(string text, TextFormat format)
            : base(text, format, getBounds(text, format))
        { }

        public static XNATextLayout[] layoutText(string text, TextFormat format, TextWrap wrap)
        {
            SpriteFont font = ((XNAFont) format.font).fontInfo.font;

            float lw = wrap.width;

            text = normalizeEOL(text);
            char[] bChars = new char[] { ' ', '\t', '-' };
            List<string> lines = new List<string>();
            string[] paragraphs = text.Split(new char[] { '\n' });
            foreach (string paragraph in paragraphs)
            {
                string[] words = splitWithDelim(paragraph, bChars);
                string line = "";
                foreach (string w in words)
                {
                    string word = w;
                    if (word.Length == 0) continue;
                    while (true)
                    {
                        string lineWithWord = line + word;
                        if (font.MeasureString(lineWithWord).X > lw)
                        {
                            if (line.Length == 0)
                            {
                                if (word.Length == 1)
                                {
                                    lines.Add(word);
                                    continue;
                                }
                                for (int i = 2; i <= word.Length; i++)
                                {
                                    string partial = word.Substring(0, i);
                                    if (font.MeasureString(paragraph).X > lw)
                                    {
                                        lines.Add(word.Substring(0, i - 1));
                                        word = word.Substring(i - 1);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                lines.Add(line);
                                line = word;
                            }
                        }
                        else
                        {
                            line = lineWithWord;
                        }
                        break;
                    }
                }
                lines.Add(line);
            }

            XNATextLayout[] layouts = new XNATextLayout[lines.Count];
            for (int i = 0; i < layouts.Length; i++)
            {
                layouts[i] = new XNATextLayout(lines[i], format);
            }
            return layouts;
        }

        public static string[] splitWithDelim(string @this, char[] delimiters)
        {
            var splits = new List<string>();
            int next, start = 0;
            while ((next = @this.IndexOfAny(delimiters, start)) >= 0)
            {
                splits.Add(@this.Substring(start, next - start + 1));
                start = next + 1;
            }
            splits.Add(@this.Substring(start));
            return splits.ToArray();
        }

        private static Rectangle getBounds(string text, TextFormat format)
        {
            if (text == null || text.Length == 0) text = " ";
            XNAFont font = (XNAFont)format.font;
            Microsoft.Xna.Framework.Vector2 size = font.fontInfo.font.MeasureString(text);
            float scale = font.size() / font.fontInfo.size;
            return new Rectangle(0, 0, size.X * scale, size.Y * scale);
        }

        public override float ascent()
        {
            return bounds().height() * 0.8f;
        }

        public override float descent()
        {
            return bounds().height() - ascent();
        }

        public override float leading()
        {
            return 0;
        }
    }
}
