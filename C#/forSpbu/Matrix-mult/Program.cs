using Matrix_mult;

var matrixLines = new string[] { "1 2", "2 3" };
var firMatrix = new Matrix();
firMatrix.Parse(matrixLines);
var secMatrix = new Matrix();
secMatrix.Parse(matrixLines);
var result = MatrixMultiplier.MultiThreaded(firMatrix, secMatrix);
result.Print();
