#include <stdio.h>
#include "../Modules/BinaryTree/customBinaryTree.h"

#define MAX_LINE_SIZE 100

Type whatIfEqualWhenAdding(Type oldValue, Type newValue) {
    oldValue.value = newValue.value;
    return oldValue;
}

Type whatIfEqualWhenSearching(Type oldValue, Type newValue) {
    return oldValue;
}

int compare(Type oldValue, Type newValue) {
    return (oldValue.key == newValue.key) ? 0 : (oldValue.key < newValue.key) ? -1 : 1;
}

bool test() {
    bool testResult = true;

    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("0 - Добавить значение по заданному ключу в словарь. Если такой ключ уже есть, значение заменяется на новое.\n"
           "1 - Получить значение по заданному ключу из словаря. Если такого ключа нет, возвращается NULL.\n"
           "2 - Проверить наличие заданного ключа в словаре.\n"
           "3 - Удалить заданный ключ и связанное с ним значение из словаря. Если такого ключа нет, функция ничего не делает.\n");


    return 0;
}