#include <stdio.h>

int main() {
    const int numberOfDigits = 10;
    int numOfLuckyTickets = 0;
    int numbersOfTripletsWithSumEqualsI[28] = {0};

    for (int firstDigit = 0; firstDigit < numberOfDigits; firstDigit++) {
        for (int secondDigit = 0; secondDigit < numberOfDigits; secondDigit++) {
            for (int thirdDigit = 0; thirdDigit < numberOfDigits; thirdDigit++) {
                numbersOfTripletsWithSumEqualsI[firstDigit + secondDigit + thirdDigit] += 1;
            }
        }
    }

    const int numOfTripletSums = 28;
    for (int i = 0; i < numOfTripletSums; i++) {
        numOfLuckyTickets += numbersOfTripletsWithSumEqualsI[i] * numbersOfTripletsWithSumEqualsI[i];
    }

    printf("Number of lucky tickets = %d", numOfLuckyTickets);

    return 0;
}
