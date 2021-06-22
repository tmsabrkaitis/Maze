using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Cell Up { get; set; }
        public Cell Down { get; set; }
        public Cell Left { get; set; }
        public Cell Right { get; set; }

        public List<Cell> Neighbhours
        {
            get { return new[] { Up, Down, Right, Left }.Where(c => c != null).ToList(); }
        }

        private Dictionary<Cell, bool> _paths;

        public Cell(int row, int col)
        {
            Row = row;
            Column = col;
            _paths = new Dictionary<Cell, bool>();
        }

        public bool IsConnected(Cell cell)
        {
            if (cell == null)
            {
                return false;
            }
            return _paths.ContainsKey(cell);
        }

        public void Connect(Cell cell, bool bidirectional = true)
        {
            _paths[cell] = true;
            if (bidirectional)
            {
                cell.Connect(this, false);
            }
        }

    }
}
