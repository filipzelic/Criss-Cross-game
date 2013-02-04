using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KrizicKruzic
{
    class Window
    {

        //privatna varijabla s get 
        //i set metodom
        private string title;

        public string GetTitle()
        {
            return title;
        }

        public void SetTitle(string newtitle)
        {
            title = newtitle;
        }

        //property s get i set metodom
        public string Oznaka { get; set; }

        //koordinate prozora
        public Point Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// Boja pozadine
        /// </summary>
        public ConsoleColor Background { get; set; }

        /// <summary>
        /// Je li prozor aktivan
        /// 
        /// Aktivnom prozoru je boja pozadine definirana
        /// s <see cref="Background"/> bojom, 
        /// dok je boja pozadine neaktivnog prozora
        /// ConsoleColor.Gray
        /// </summary>
        public bool Active { get; set; }
    }
}
