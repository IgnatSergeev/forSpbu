#include <string.h>
#include <stdio.h>
#include <stdbool.h>
#include <malloc.h>

#define MAX_STRING_SIZE 100
#define P 13
#define M 12912391

int calculateSampleHash(char sampleString[]) {
    int stringSize = (int)strlen(sampleString);
    int hash = sampleString[0] % M;
    for (int i = 1; i < stringSize; i++) {
        hash = ((hash * P) % M + sampleString[i]) % M;
    }

    return hash;
}

void calculateHashArray(int hashArray[], char string[], int stringSize) {
    hashArray[0] = string[0] % M;
    for (int i = 1; i < stringSize; i++) {
        hashArray[i] = ((hashArray[i - 1] * P) % M + string[i]) % M;
    }
}

void calculateDegreeArray(int degreeArray[], int arraySize) {
    degreeArray[0] = 1;
    for (int i = 1; i < arraySize; i++) {
        degreeArray[i] = (degreeArray[i-1] * P) % M;
    }
}

int findFirstSampleStringInText(FILE *file, char sampleString[]) {
    int sampleHash = calculateSampleHash(sampleString);
    int sampleLength = (int)strlen(sampleString);

    int degreeArray[MAX_STRING_SIZE] = {0};
    calculateDegreeArray(degreeArray, sampleLength);

    int startTextIndex = 0;
    char currentChar = (char)getc(file);
    while (currentChar == ' ') {
        startTextIndex += 1;
        currentChar = (char)getc(file);
    }
    ungetc(currentChar, file);

    char string[MAX_STRING_SIZE] = {0};
    int textIndex = startTextIndex;
    while (fscanf(file, "%s", string) != EOF) {
        int stringSize = (int)strlen(string);
        int hashArray[MAX_STRING_SIZE] = {0};
        calculateHashArray(hashArray, string, stringSize);
        if (sampleHash == hashArray[sampleLength - 1]) {
            bool isEqual = true;
            for (int j = 0; j < sampleLength; j++) {
                if (sampleString[j] != string[j]) {
                    isEqual = false;
                }
            }
            if (isEqual) {
                return textIndex;
            }
        }

        for (int i = 1; i < stringSize - sampleLength + 1; i++) {
            if (sampleHash == (hashArray[i + sampleLength - 1] - hashArray[i - 1] * degreeArray[sampleLength]) % M) {
                for (int j = 0; j < sampleLength; j++) {
                    if (sampleString[j] != string[j + i]) {
                        continue;
                    }
                }
                return textIndex + i;
            }
        }

        textIndex += stringSize;
        currentChar = (char)getc(file);
        while (currentChar == ' ') {
            textIndex += 1;
            currentChar = (char)getc(file);
        }
        ungetc(currentChar, file);
    }

    return -1;
}

bool test(void) {
    bool testResult = true;

    FILE *testFile = fopen("../30.11/stringSearchTest.txt", "r");
    if (testFile == NULL) {
        fclose(testFile);
        return false;
    }

    char testSampleString[MAX_STRING_SIZE] = "s3";

    if (findFirstSampleStringInText(testFile, testSampleString) != 27) {
        testResult = false;
    }

    fclose(testFile);
    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");

    FILE *file = fopen("../30.11/stringSearch.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");
        return -1;
    }

    printf("Введите строку, которую хотите найти в тексте\n");
    char sampleString[MAX_STRING_SIZE] = {0};
    scanf("%s", sampleString);

    int textIndex = findFirstSampleStringInText(file, sampleString);
    fclose(file);

    if (textIndex == -1) {
        printf("Данная строка не встречается в тексте\n");
    }
    printf("Вот индекс, с которого начинается строка в тексте - %d", textIndex);

    return 0;
}