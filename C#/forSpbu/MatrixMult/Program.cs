using MatrixMult;

var matrixLines = new string[] { "1 2", "2 3" };
var firMatrix = new Matrix(matrixLines);
var secMatrix = new Matrix(matrixLines);
var result = MatrixMultiplier.MultiThreadedMultiply(firMatrix, secMatrix);
result.Print();
