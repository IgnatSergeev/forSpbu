#include "hashMap.h"
#include <stdio.h>

int main() {
    HashMap *hashMap = createHashMap();
    char array1[] = "abcdec";
    char array2[] = "abcdec";
    char array3[] = "abcded";
    printf("%d", hashFunction(array1));
    addValue(hashMap, array1, 6, NULL);
    addValue(hashMap, array2, 6, NULL);
    addValue(hashMap, array3, 6, NULL);

    deleteValue(hashMap, array1);

    return 0;
}