#pragma once

#include <stdbool.h>
#include "typeDef.h"

typedef struct HashMap HashMap;

HashMap *createHashMap();

int addValue(HashMap *hashMap, Type key);

void deleteValue(HashMap *hashMap, Type key);

void clearHashMap(HashMap *hashMap);

int calculateFillFactor(HashMap *hashMap);

int calculateMaxListSize(HashMap *hashMap);

int calculateMiddleListSize(HashMap *hashMap);