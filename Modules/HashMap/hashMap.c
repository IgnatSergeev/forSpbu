#include "hashMap.h"
#include "customListForHashMap.h"
#include <string.h>

struct HashMap {
    List **hashArray;
    int *arrayOfNumberOfElementsInHashArray;
    int hashMapSize;
    int numberOfElementsInHashMap;
};

HashMap *createHashMap() {
    HashMap *hashMap = calloc(1, sizeof(HashMap));
    if (hashMap == NULL) {
        return NULL;
    }
    hashMap->hashMapSize = 1;
    hashMap->hashArray = calloc(1, sizeof(List *));
    if (hashMap->hashArray == NULL) {
        free(hashMap);
        return NULL;
    }
    hashMap->arrayOfNumberOfElementsInHashArray = calloc(1, sizeof(int));
    if (hashMap->arrayOfNumberOfElementsInHashArray == NULL) {
        free(hashMap->hashArray);
        free(hashMap);
        return NULL;
    }


    for (int i = 0; i < hashMap->hashMapSize; i++) {
        List *currentList = create();
        if (currentList == NULL) {
            for (int j = 0; j < i; j++) {
                clear(hashMap->hashArray[j]);
            }
            free(hashMap->hashArray);
            free(hashMap->arrayOfNumberOfElementsInHashArray);
            free(hashMap);
            return NULL;
        }
        hashMap->hashArray[i] = currentList;
    }

    return hashMap;
}

void clearHashMap(HashMap *hashMap) {
    for (int i = 0; i < hashMap->hashMapSize; i++) {
        clear(hashMap->hashArray[i]);
    }
    free(hashMap->hashArray);
    free(hashMap->arrayOfNumberOfElementsInHashArray);
    free(hashMap);
}

void resize(HashMap *hashMap) {
    int oldHashMapSize = hashMap->hashMapSize;
    hashMap->hashMapSize *= 2;
    List **oldHashArray = hashMap->hashArray;
    int *oldArrayOfNumberOfElementsInHashArray = hashMap->arrayOfNumberOfElementsInHashArray;
    hashMap->hashArray = calloc(hashMap->hashMapSize, sizeof(List *));
    hashMap->arrayOfNumberOfElementsInHashArray = calloc(hashMap->hashMapSize, sizeof(int));

    for (int i = 0; i < oldHashMapSize; i++) {
        while (!isEmpty(oldHashArray[i])) {
            Type value = deleteNode(oldHashArray[i], 0);
            addValue(hashMap, value, )
        }
    }
}

int addValue(HashMap *hashMap, Type key, int keySize) {
    int hashFunctionValue = hashFunction(key) % hashMap->hashMapSize;
    List *listToAddValue = hashMap->hashArray[hashFunctionValue];

    int indexOfValueInListIfExist = findNodeIndexByValue(listToAddValue, key);
    if (indexOfValueInListIfExist == -1) {
        int returnValue = insertNodeToEnd(listToAddValue, key, keySize, 1);
        if (!returnValue) {
            hashMap->arrayOfNumberOfElementsInHashArray[hashFunctionValue] += 1;
            hashMap->numberOfElementsInHashMap += 1;
        }
        if (hashMap->hashMapSize == hashMap->numberOfElementsInHashMap) {
            resize(hashMap);
        }
        return returnValue;
    } else {
        changeNode(listToAddValue, indexOfValueInListIfExist, key);
        return 0;
    }
}

void deleteValue(HashMap *hashMap, Type key) {
    int hashFunctionOfValueToDelete = hashFunction(key);
    List *listToDeleteIn = hashMap->hashArray[hashFunctionOfValueToDelete];

    int indexToDelete = findNodeIndexByValue(listToDeleteIn, key);
    if (indexToDelete == -1) {
        return;
    }
    deleteNode(listToDeleteIn, indexToDelete);
    hashMap->numberOfElementsInHashMap -= 1;
    hashMap->arrayOfNumberOfElementsInHashArray[hashFunctionOfValueToDelete] -= 1;
}



