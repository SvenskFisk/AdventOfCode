namespace a
{
    class Robot
    {
        public Robot()
        {
            Pos = (0, 0);
            Dir = Direction.Up;
        }

        public (int x, int y) Pos { get; private set; }

        public Direction Dir { get; private set; }

        public void Turn(long command)
        {
            var newDir = (long)Dir + (command == 1 ? 1 : -1);
            Dir = (Direction)((newDir + 4) % 4);

            (int x, int y) move = Dir switch
            {
                Direction.Up => (0, 1),
                Direction.Down => (0, -1),
                Direction.Left => (-1, 0),
                Direction.Right => (1, 0)
            };

            Pos = (Pos.x + move.x, Pos.y + move.y);
        }
    }
}
