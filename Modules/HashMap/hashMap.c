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

int addValue(HashMap *hashMap, KeyType key, Type value) {
    int hashFunctionValue = hashFunction(key);
    List *listToAddValue = hashMap->hashArray[hashFunctionValue];

    int returnValue = insertNodeToEnd(listToAddValue, value);
    if (!returnValue) {
        hashMap->arrayOfNumberOfElementsInHashArray[hashFunctionValue] += 1;
        hashMap->numOfElementsInHashMap += 1;
    }
    return returnValue;
}

void deleteValue(HashMap *hashMap, KeyType key, Type value) {
    int hashFunctionOfValueToDelete = hashFunction(key);
    List *listToDeleteIn = hashMap->hashArray[hashFunctionOfValueToDelete];

    int indexToDelete = findNodeIndexByValue(listToDeleteIn, value);
    if (indexToDelete == -1) {
        return ;
    }
    deleteNode(listToDeleteIn, indexToDelete);
}



