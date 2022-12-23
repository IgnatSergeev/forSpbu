#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>

#define MAX_MATRIX_SIZE 15

void parseMatrixIntoDotFile(int **matrix, int size) {
    FILE *file = fopen("../30.11/graphViz.dot", "w");
    fprintf(file, "digraph Graph {\n");
    for (int i = 0; i < size; i++) {
        for (int j = 0; j < size; j++) {
            if (matrix[i][j] != -1) {
                fprintf(file, "%d -> %d[label = \"%d\"];\n", i, j, matrix[i][j]);
            }
        }
    }
    fprintf(file, "}");
}

int findIntSqrt(int square) {
    for (int i = 0; i < square; i++) {
        if (i * i == square) {
            return i;
        }
        if (i * i > square) {
            return -1;
        }
    }
    return -1;
}

int main(void) {
    //elements on main diagonal should be equal to -1
    //there cant be edges with length 0

    FILE *file = fopen("../30.11/adjacencyMatrix.txt", "r");
    int currentLength = 0;
    int index = 0;
    int linearMatrix[MAX_MATRIX_SIZE * MAX_MATRIX_SIZE] = {0};
    while (fscanf(file, "%d", &currentLength) != EOF) {
        linearMatrix[index] = currentLength;
        ++index;
    }

    int size = findIntSqrt(index);
    if (size == -1) {
        printf("Матрица должна быть квадратной\n");
        return -1;
    }

    int **matrix = calloc(size, sizeof(int *));
    for (int i = 0; i < size; i++) {
        matrix[i] = calloc(size, sizeof(int));
        for (int j = 0; j < size; j++) {
            matrix[i][j] = linearMatrix[i * size + j];
        }
    }

    parseMatrixIntoDotFile(matrix, size);

    system("cd ..\\30.11 && graphViz.bat");

    for (int i = 0; i < size; i++) {
        free(matrix[i]);
    }
    free(matrix);
    return 0;
}