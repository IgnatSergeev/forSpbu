#include <stdbool.h>
#include <stdio.h>
#include <string.h>

#define FIRST_DIMENSION_SIZE 4
#define SECOND_DIMENSION_SIZE 3
#define MAX_STRING_SIZE 100

typedef enum State {
    waiting,
    supposingCommentaryStart,
    commentary,
    supposingCommentaryEnd
} State;

int getTableIndex(char currentChar) {
    if (currentChar == '/') {
        return 0;
    }
    if (currentChar == '*') {
        return 1;
    }
    return 2;
}

int getRowIndex(State state) {
    switch (state) {
        case waiting: {
            return 0;
        } case supposingCommentaryStart: {
            return 1;
        } case commentary: {
            return 2;
        } default: {
            return 3;
        }
    }
}

State getStateByIndex(int index) {
    if (index == 0) {
        return waiting;
    }
    if (index == 1) {
        return supposingCommentaryStart;
    }
    if (index == 2) {
        return commentary;
    }
    return supposingCommentaryEnd;
}

void parseStateMatrix(FILE *file, int stateMatrix[FIRST_DIMENSION_SIZE][SECOND_DIMENSION_SIZE]) {
    for (int i = 0; i < FIRST_DIMENSION_SIZE; i++) {
        for (int j = 0; j < SECOND_DIMENSION_SIZE; j++) {
            int currentTransition = 0;
            fscanf(file, "%d", &currentTransition);
            stateMatrix[i][j] = currentTransition;
        }
    }
}

int processTheText(FILE *file, int stateMatrix[FIRST_DIMENSION_SIZE][SECOND_DIMENSION_SIZE], char outputString[MAX_STRING_SIZE]) {
    int outputStringIndex = 0;

    char currentChar = (char)fgetc(file);
    State state = waiting;
    while (currentChar != EOF) {
        int tableIndex = getTableIndex(currentChar);
        int rowIndex = getRowIndex(state);
        State previousState = state;
        state = getStateByIndex(stateMatrix[rowIndex][tableIndex]);
        if (state == commentary) {
            if (previousState == supposingCommentaryStart) {
                if (outputStringIndex >= MAX_STRING_SIZE) {
                    return -1;
                }
                outputString[outputStringIndex] = '/';
                ++outputStringIndex;
            }
            if (outputStringIndex >= MAX_STRING_SIZE) {
                return -1;
            }
            outputString[outputStringIndex] = currentChar;
            ++outputStringIndex;
        }
        if (state == supposingCommentaryEnd && previousState == commentary) {
            if (outputStringIndex >= MAX_STRING_SIZE) {
                return -1;
            }
            outputString[outputStringIndex] = currentChar;
            ++outputStringIndex;
        }
        if (state == waiting && previousState == supposingCommentaryEnd) {
            if (outputStringIndex >= MAX_STRING_SIZE) {
                return -1;
            }
            outputString[outputStringIndex] = '/';
            ++outputStringIndex;
        }
        if (state == supposingCommentaryEnd && previousState == supposingCommentaryEnd) {
            if (outputStringIndex >= MAX_STRING_SIZE) {
                return -1;
            }
            outputString[outputStringIndex] = currentChar;
            ++outputStringIndex;
        }
        currentChar = (char)fgetc(file);
    }

    return 0;
}

bool test(void) {
    FILE *testFile = fopen("../07.12/commentaryLexerStateMatrix.txt", "r");
    if (testFile == NULL) {
        return false;
    }
    int testStateMatrix[FIRST_DIMENSION_SIZE][SECOND_DIMENSION_SIZE] = {0};
    parseStateMatrix(testFile, testStateMatrix);
    fclose(testFile);

    FILE *textTestFile = fopen("../07.12/textTest.txt", "r");
    if (textTestFile == NULL) {
        return false;
    }
    char outputTestString[MAX_STRING_SIZE] = {0};
    int errorCode = processTheText(textTestFile, testStateMatrix, outputTestString);
    fclose(textTestFile);
    if (errorCode) {
        return false;
    }

    return !strcmp(outputTestString, "/**a**sa////sd**/");
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");

    FILE *file = fopen("../07.12/commentaryLexerStateMatrix.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");
        return -1;
    }
    int stateMatrix[FIRST_DIMENSION_SIZE][SECOND_DIMENSION_SIZE] = {0};
    parseStateMatrix(file, stateMatrix);
    fclose(file);

    FILE *textFile = fopen("../07.12/text.txt", "r");
    if (textFile == NULL) {
        printf("Файл не найден\n");
        return -1;
    }
    char outputString[MAX_STRING_SIZE] = {0};
    int errorCode = processTheText(textFile, stateMatrix, outputString);
    fclose(textFile);
    if (errorCode) {
        printf("Слишком много символов в комментариях\n");
        return -1;
    }
    printf("%s", outputString);

    return 0;
}