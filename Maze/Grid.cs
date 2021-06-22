using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    class Grid
    {
        public int Rows { get; }
        public int Columns { get; }

        private List<List<Cell>> _grid;

        public Cell this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Rows)
                {
                    return null;
                }
                if (column < 0 || column >= Columns)
                {
                    return null;
                }
                return _grid[row][column];
            }
        }

        public IEnumerable<List<Cell>> Row
        {
            get
            {
                foreach (var row in _grid)
                {
                    yield return row;
                }
            }
        }

        public IEnumerable<Cell> Cells
        {
            get
            {
                foreach (var row in Row)
                {
                    foreach (var cell in row)
                    {
                        yield return cell;
                    }
                }
            }
        }

        public Grid(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;

            PrepareGrid();
            ConfigureCells();
        }

        public override string ToString()
        {
            var output = new StringBuilder("#");
            for (var i = 0; i < Columns; i++)
            {
                output.Append("####");
            }
            output.AppendLine();

            foreach (var row in Row)
            {
                var top = "#";
                var bottom = "#";
                foreach (var cell in row)
                {
                    const string body = "   ";
                    var right = cell.IsConnected(cell.Right) ? " " : "#";

                    top += body + right;

                    var down = cell.IsConnected(cell.Down) ? "   " : "###";
                    const string corner = "#"; 
                    bottom += down + corner;
                }
                output.AppendLine(top);
                output.AppendLine(bottom);
            }

            return output.ToString();
        }

        private void PrepareGrid()
        {
            _grid = new List<List<Cell>>();
            for (var r = 0; r < Rows; r++)
            {
                var row = new List<Cell>();
                for (var c = 0; c < Columns; c++)
                {
                    row.Add(new Cell(r, c));
                }
                _grid.Add(row);
            }
        }

        private void ConfigureCells()
        {
            foreach (var cell in Cells)
            {
                cell.Up = this[cell.Row - 1, cell.Column];
                cell.Down = this[cell.Row + 1, cell.Column];
                cell.Left = this[cell.Row, cell.Column - 1];
                cell.Right = this[cell.Row, cell.Column + 1];
            }
        }

        public static Grid Maze(Grid grid, int seed = -1)
        {
            var rand = seed >= 0 ? new Random(seed) : new Random();
            foreach (var cell in grid.Cells)
            {
                var neighbors = new[] { cell.Up, cell.Right }.Where(c => c != null).ToList();
                if (!neighbors.Any())
                {
                    continue;
                }
                var neighbor = neighbors[rand.Next(neighbors.Count)];
                if (neighbor != null)
                {
                    cell.Connect(neighbor);
                }
            }

            return grid;
        }


    }
}
