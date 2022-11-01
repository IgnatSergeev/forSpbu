#include "sortedList.h"

typedef struct Node {
    int value;
    struct Node *next;
} Node;

struct List {
    Node *head;
};

