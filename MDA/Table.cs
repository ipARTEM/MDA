using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDA
{
    public class Table
    {
        public State State { get;  set; }
        public int Id { get; }
        public int SeatsCount { get; } = 4;

        public Table(int id)
        {
            Id = id;
            State = State.Free;
            //SeatsCount = Random.Next(2,5);
        }

        public bool SetState(State state)
        {
            if (state== State)
            {
                return false;
            }

            State = state;
            return true;
        }

    }
}
