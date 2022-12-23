#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <string.h>

#define MAX_MATRIX_SIZE 15
#define MAX_STRING_SIZE 100

void parseMatrixIntoDotFile(int **matrix, int size, char filename[]) {
    FILE *file = fopen(filename, "w");
    fprintf(file, "digraph Graph {\n");
    for (int i = 0; i < size; i++) {
        for (int j = 0; j < size; j++) {
            if (matrix[i][j] != -1) {
                fprintf(file, "%d -> %d[label = \"%d\"];\n", i, j, matrix[i][j]);
            }
        }
    }
    fprintf(file, "}");
    fclose(file);
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

int **readMatrix(FILE *file, int *size) {
    int currentLength = 0;
    int index = 0;
    int linearMatrix[MAX_MATRIX_SIZE * MAX_MATRIX_SIZE] = {0};
    while (fscanf(file, "%d", &currentLength) != EOF) {
        linearMatrix[index] = currentLength;
        ++index;
    }

    *size = findIntSqrt(index);

    int **matrix = calloc(*size, sizeof(int *));
    for (int i = 0; i < *size; i++) {
        matrix[i] = calloc(*size, sizeof(int));
        for (int j = 0; j < *size; j++) {
            matrix[i][j] = linearMatrix[i * *size + j];
        }
    }

    return matrix;
}

bool test(void) {
    bool testResult = true;
    FILE *testFile = fopen("../30.11/adjacencyMatrixTest.txt", "r");
    if (testFile == NULL) {
        return false;
    }
    int size = 0;
    int **matrix = readMatrix(testFile, &size);
    fclose(testFile);

    for (int i = 0; i < size; i++) {
        for (int j = 0; j < size; j++) {
            if (i == size - j - 1) {
                if (matrix[i][j] != 1) {
                    testResult = false;
                }
                continue;
            }
            if (matrix[i][j] != -1) {
                testResult = false;
            }
        }
    }

    for (int i = 0; i < size; i++) {
        free(matrix[i]);
    }
    free(matrix);
    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");

    printf("Программа прочитает матрицу смежности из файла adjacencyMatrix.txt и откроет визуализированную матрицу "
           "(в ней не может быть нулей и элементы на главной диагонали должны быть  равны -1) \n");

    FILE *file = fopen("../30.11/adjacencyMatrix.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");
        return -1;
    }
    int size = 0;
    int **matrix = readMatrix(file, &size);
    fclose(file);

    parseMatrixIntoDotFile(matrix, size, "../30.11/graphViz.dot");

    system("cd ..\\30.11 && dot -Tpng graphViz.dot -o graphViz.png && timeout 1 && start graphViz.png");

    for (int i = 0; i < size; i++) {
        free(matrix[i]);
    }
    free(matrix);
    return 0;
}