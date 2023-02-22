#include <stdio.h>
#include <stdbool.h>

#define MAX_FILE_SIZE 1000

int readFromFileWithCheckingOnDuplicates(FILE *file, char *output) {
    if (file == NULL) {
        return 1;
    }
    int outputIndex = 0;
    char previousChar = 0;
    char currentChar = 0;
    while (!feof(file)) {
        previousChar = currentChar;
        currentChar = (char)fgetc(file);
        if (previousChar != currentChar && currentChar != -1) {
            output[outputIndex] = currentChar;
            ++outputIndex;
        }
    }

    return 0;
}

bool test(void) {
    bool testResult = true;
    FILE *testFile = fopen("../KR3/test.txt", "r");
    char testOutput[MAX_FILE_SIZE] = {'\0'};
    int errorCode = readFromFileWithCheckingOnDuplicates(testFile, testOutput);
    if (errorCode) {
        printf("Ошибка с нахождением файла с тестом");
        return false;
    }
    char correctTestOutput[] = "acdefg";
    int correctTestOutputSize = 6;

    for (int i = 0; i < correctTestOutputSize; i++) {
        if (testOutput[i] != correctTestOutput[i]) {
            testResult = false;
            break;
        }
    }
    if (testOutput[correctTestOutputSize] != '\0') {
        testResult = false;
    }
    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите в файл input.txt нужный текст, в котором хотите заменить последовательности повторяющихся"
           " символов одним символом\n");
    FILE *inputFile = fopen("../KR3/input.txt", "r");
    char output[MAX_FILE_SIZE] = {'\0'};
    int errorCode = readFromFileWithCheckingOnDuplicates(inputFile, output);
    if (errorCode) {
        printf("Ошибка с нахождением файла\n");
        return -1;
    }
    printf("Вот получившийся текст после замены:\n%s", output);
    return 0;
}