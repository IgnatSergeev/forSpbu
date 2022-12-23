#pragma once

#include <stdbool.h>
#include "typeDef.h"

typedef struct HashMap HashMap;

HashMap *createHashMap();

int addValue(HashMap *hashMap, Type key);

void print(HashMap *hashMap);

bool isEmpty(HashMap *hashMap);

void deleteValue(HashMap *hashMap, Type key);

void clearHashMap(HashMap *hashMap);

float calculateFillFactor(HashMap *hashMap);

int calculateMaxListSize(HashMap *hashMap);

int calculateAverageListSize(HashMap *hashMap);

Type findValue(HashMap *hashMap, Type value);