#include "customList.h"
#include <stdio.h>
#include <string.h>

typedef struct Node {
    Type value;
    int frequency;
    struct Node *next;
} Node;

struct List {
    Node *head;

    int listSize;
};

/*void print(List *list) {
    Node *temp = list->head;
    while (temp->next != NULL) {
        printf("%d ", temp->value);
        temp = temp->next;
    }
    printf("%d\n", temp->value);
}*/


int insertNode(List *list, Type value, int keySize, int index) {

    if (index < 0) {
        return -1;
    }
    if (index == 0) {

        Node *newNode = calloc(1, sizeof(Node));

        if (newNode == NULL) {
            return -1;
        }
        newNode->next = list->head;

        newNode->value = calloc(keySize, sizeof(char));
        strcpy(newNode->value, value);
        newNode->frequency = 1;
        list->head = newNode;
        list->listSize += 1;

        return 0;
    }
    if (isEmpty(list)) {
        return -1;
    }

    Node *iteratorNode = list->head;
    int currentIndex = 0;
    while (currentIndex != index - 1) {
        if (iteratorNode->next == NULL) {
            return -1;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }

    Node *newNode = malloc(sizeof(Node));
    if (newNode == NULL) {
        return -1;
    }

    strcpy(newNode->value, value);
    newNode->next = iteratorNode->next;
    iteratorNode->next = newNode;
    list->listSize += 1;
    return 0;
}

int insertNodeToEnd(List *list, Type value, int keySize) {
    return insertNode(list, value, keySize, list->listSize);
}

List *create() {
    List *list = malloc(sizeof(List));
    if (list == NULL) {
        return NULL;
    }
    list->head = NULL;
    list->listSize = 0;

    return list;
}

int deleteNode(List* list, int index) {
    if (isEmpty(list)) {
        return -1;
    }
    if (index < 0) {
        return -1;
    }
    if (index == 0) {
        Node *nodeToDelete = list->head;
        list->head = nodeToDelete->next;
        free(nodeToDelete);

        list->listSize -= 1;

        return 0;
    }

    Node *iteratorNode = list->head;
    int currentIndex = 0;
    while (currentIndex != index - 1) {
        if (iteratorNode->next == NULL) {
            return -1;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }
    Node *nodeToDelete = iteratorNode->next;
    iteratorNode->next = nodeToDelete->next;
    free(nodeToDelete);

    list->listSize -= 1;

    return 0;
}

Type findNode(List *list, int index, int *errorCode) {
    if (isEmpty(list) || index < 0) {
        *errorCode = -1;
        return (Type)0;
    }
    Node *iteratorNode = list->head;

    for (int i = 0; i < index; ++i) {
        iteratorNode = iteratorNode->next;
        if (iteratorNode == NULL) {
            *errorCode = -1;
            return (Type)0;
        }
    }

    *errorCode = 0;
    return iteratorNode->value;
}


int findNodeIndexByValue(List *list, Type value) {
    if (isEmpty(list)) {
        return -1;
    }
    Node *iteratorNode = list->head;

    int currentIndex = 0;
    while (iteratorNode != NULL) {
        if (!strcmp(iteratorNode->value, value)) {
            break;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }

    if (iteratorNode == NULL) {
        return -1;
    }
    return currentIndex;
}


bool isEmpty(List *list) {
    return list->head == NULL;
}

void clear(List *list) {
    while (!isEmpty(list)) {
        deleteNode(list, 0);
    }
    free(list);
}

int changeNode(List *list, int index, Type value) {
    if (index < 0) {
        return -1;
    }
    if (isEmpty(list)) {
        return -1;
    }

    Node *iteratorNode = list->head;
    int currentIndex = 0;
    while (currentIndex != index) {
        if (iteratorNode->next == NULL) {
            return -1;
        }
        ++currentIndex;
        iteratorNode = iteratorNode->next;
    }


    strcpy(iteratorNode->value, value);
    ++iteratorNode->frequency;

    return 0;
}

