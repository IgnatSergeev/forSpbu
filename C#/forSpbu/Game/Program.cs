using Game;

var eventLoop = new EventLoop();
var game = new Game.Game();

eventLoop.StartHandler += game.OnStart;
eventLoop.EndHandler += Game.Game.OnEnd;

eventLoop.LoopStartHandler += game.OnLoopStart;

eventLoop.LeftHandler += game.OnLeft;
eventLoop.RightHandler += game.OnRight;
eventLoop.DownHandler += game.OnDown;
eventLoop.UpHandler += game.OnUp;

eventLoop.Run();