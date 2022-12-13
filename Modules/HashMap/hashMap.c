#include "hashMap.h"
#include "../ListModule/customList.h"

struct HashMap {
    List *hashArray[HASH_FUNCTION_RANGE];
    int arrayOfNumberOfElementsInHashArray[HASH_FUNCTION_RANGE];
    int numOfElementsInHashMap;
};

HashMap *createHashMap() {
    HashMap *hashMap = calloc(1, sizeof(HashMap));
    if (hashMap == NULL) {
        return NULL;
    }

    for (int i = 0; i < HASH_FUNCTION_RANGE; i++) {
        List *currentList = create();
        if (currentList == NULL) {
            for (int j = 0; j < i; j++) {
                clear(hashMap->hashArray[j]);
                free(hashMap);
                return NULL;
            }
        }
        hashMap->hashArray[i] = currentList;
    }

    return hashMap;
}

void clearHashMap(HashMap *hashMap) {
    for (int i = 0; i < HASH_FUNCTION_RANGE; i++) {
        clear(hashMap->hashArray[i]);
    }
    free(hashMap);
}

int addValue(HashMap *hashMap, Type key, int keySize, Type (*whatToDoIfAlreadyExist)(Type)) {
    int hashFunctionValue = hashFunction(key);
    List *listToAddValue = hashMap->hashArray[hashFunctionValue];

    int indexOfValueInListIfExist = findNodeIndexByValue(listToAddValue, key);
    if (indexOfValueInListIfExist == -1) {
        int returnValue = insertNodeToEnd(listToAddValue, key, keySize);
        if (!returnValue) {
            hashMap->arrayOfNumberOfElementsInHashArray[hashFunctionValue] += 1;
            hashMap->numOfElementsInHashMap += 1;
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
    hashMap->numOfElementsInHashMap -= 1;
    hashMap->arrayOfNumberOfElementsInHashArray[hashFunctionOfValueToDelete] -= 1;
}



