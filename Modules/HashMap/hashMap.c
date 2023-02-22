#include "hashMap.h"
#include "customListForHashMap.h"

struct HashMap {
    List **hashArray;
    int *arrayOfNumberOfElementsInHashArray;
    int hashMapSize;
    int numberOfElementsInHashMap;
};

bool isEmpty(HashMap *hashMap) {
    return hashMap->numberOfElementsInHashMap == 0;
}

void print(HashMap *hashMap) {
    for (int i = 0; i < hashMap->hashMapSize; i++) {
        printList(hashMap->hashArray[i]);
    }
}

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

    List *currentList = create();
    if (currentList == NULL) {
        free(hashMap->hashArray);
        free(hashMap->arrayOfNumberOfElementsInHashArray);
        free(hashMap);
        return NULL;
    }
    hashMap->hashArray[0] = currentList;

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
    List **oldHashArray = hashMap->hashArray;
    free(hashMap->arrayOfNumberOfElementsInHashArray);

    hashMap->hashMapSize *= 2;
    hashMap->numberOfElementsInHashMap = 0;
    hashMap->hashArray = calloc(hashMap->hashMapSize, sizeof(List *));
    for (int i = 0; i < hashMap->hashMapSize; i++) {
        hashMap->hashArray[i] = create();
    }
    hashMap->arrayOfNumberOfElementsInHashArray = calloc(hashMap->hashMapSize, sizeof(int));

    for (int i = 0; i < oldHashMapSize; i++) {
        while (!isListEmpty(oldHashArray[i])) {
            Type value = deleteNode(oldHashArray[i], 0);
            addValue(hashMap, value);
        }
        clear(oldHashArray[i]);
    }
    free(oldHashArray);
}

int addValue(HashMap *hashMap, Type value) {
    int hashFunctionValue = abs(hashFunction(value)) % hashMap->hashMapSize;
    List *listToAddValue = hashMap->hashArray[hashFunctionValue];

    int indexOfValueInListIfExist = findNodeIndexByValue(listToAddValue, value);
    if (indexOfValueInListIfExist == -1) {
        int returnValue = insertNodeToEnd(listToAddValue, value);
        if (!returnValue) {
            hashMap->arrayOfNumberOfElementsInHashArray[hashFunctionValue] += 1;
            hashMap->numberOfElementsInHashMap += 1;
        }
        if (hashMap->hashMapSize == hashMap->numberOfElementsInHashMap) {
            resize(hashMap);
        }
        return returnValue;
    } else {
        changeNodeValueByOne(listToAddValue, indexOfValueInListIfExist);
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

float calculateFillFactor(HashMap *hashMap) {
    float result = (float)hashMap->numberOfElementsInHashMap / (float)hashMap->hashMapSize;
    return result;
}

int calculateMaxListSize(HashMap *hashMap) {
    int maxListSize = 0;
    for (int i = 0; i < hashMap->hashMapSize; i++) {
        if (hashMap->arrayOfNumberOfElementsInHashArray[i] > maxListSize) {
            maxListSize = hashMap->arrayOfNumberOfElementsInHashArray[i];
        }
    }
    return maxListSize;
}

int calculateAverageListSize(HashMap *hashMap) {
    int sumListSize = 0;
    for (int i = 0; i < hashMap->hashMapSize; i++) {
        sumListSize += hashMap->arrayOfNumberOfElementsInHashArray[i];
    }
    return sumListSize / hashMap->hashMapSize;//equal to fill factor)))
}

Type findValue(HashMap *hashMap, Type value) {
    Type nullValue = {0};
    int hashFunctionValue = abs(hashFunction(value)) % hashMap->hashMapSize;
    List *listToSearchIn = hashMap->hashArray[hashFunctionValue];

    int indexOfValueInListIfExist = findNodeIndexByValue(listToSearchIn, value);
    if (indexOfValueInListIfExist == -1) {
        return nullValue;
    }
    int errorCode = 0;
    Type newValue = findNode(listToSearchIn, indexOfValueInListIfExist, &errorCode);
    if (errorCode) {
        return nullValue;
    }
    return newValue;
}


