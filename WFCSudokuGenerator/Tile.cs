using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WFCSudokuGenerator
{
    public class Tile
    {
        public Vector2 position { get; private set; }
        public Button button { get; private set; }
        public State state { get; private set; } = State.Superposition;


        public Tile(Vector2 position, Button button)
        {
            this.position = position;
            this.button = button;
        }

        public void Collapse()
        {

        }

        public void Propagate()
        {

        }

        public enum State
        {
            Superposition,
            Fixed
        }
    }
}
