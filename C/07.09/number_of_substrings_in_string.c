#include <stdio.h>

int calculateNumberOfOccurrencesInString(const char string[1001],const char substring[1001]) {
    const int maxStringSize = 1001;
    int numberOfOccurrencesInString = 0;
    int substringLength = 0;

    for (int i = 0; i < maxStringSize; i++) {
        if (substring[i] == '\0') {
            break;
        }
        substringLength += 1;
    }
    for (int i = 0; i < maxStringSize; i++) {
        int correctLength = 0;
        while ((string[i + correctLength] != '\0') && (substring[correctLength] != '\0') && (string[i + correctLength] == substring[correctLength])) {
            correctLength += 1;
        }
        if (correctLength == substringLength) {
            numberOfOccurrencesInString += 1;
        }
    }
    return numberOfOccurrencesInString;
}

int main() {
    char string[1001] = {0};
    char substring[1001] = {0};

    printf("%s", "Введите сначала строку, в которой нужно будет искать подстроки, а потом строку, которую нужно будет искать, как подстроку(размер обоих не должен превышать 1000 элементов)\n");
    scanf("%s", string);
    scanf("%s", substring);

    int numberOfOccurrencesInString = calculateNumberOfOccurrencesInString(string, substring);
    printf("Число вхождений данной подстроки в строку = %d", numberOfOccurrencesInString);

    return 0;
}
