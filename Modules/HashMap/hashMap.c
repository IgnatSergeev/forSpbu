#include "hashMap.h"
#include "../ListModule/customList.h"

struct HashMap {
    List *hashArray[HASH_FUNCTION_RANGE];
};

HashMap *createHashMap() {
    HashMap *hashMap = malloc(sizeof(HashMap));
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

int addValue(HashMap *hashMap, Type value, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type)) {
    int hashFunctionValue = hashFunction(value);
    List *listToAddValue = hashMap->hashArray[hashFunctionValue];
    insertNode(listToAddValue, value, )
}




