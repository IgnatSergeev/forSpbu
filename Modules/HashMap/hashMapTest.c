#include "hashMap.h"
#include <stdio.h>
#include <string.h>

int main() {
    HashMap *hashMap = createHashMap();
    char array1[] = "abcdec";
    char array2[] = "abcdec";
    char array3[] = "abcded";
    Type value1 = {0};
    strcpy(value1.key, array1);
    value1.value = 1;
    Type value2 = {0};
    strcpy(value2.key, array2);
    value2.value = 1;
    Type value3 = {0};
    strcpy(value3.key, array3);
    value3.value = 1;
    addValue(hashMap, value1);
    addValue(hashMap, value2);
    addValue(hashMap, value3);
    return 0;
}