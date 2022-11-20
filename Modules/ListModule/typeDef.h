#pragma once

#define MAX_LINE_SIZE 100
#define MAX_PHONE_BOOK_SIZE 100
#define MAX_PHONE_NUMBER_SIZE 13
#define MAX_NAME_SIZE (MAX_LINE_SIZE - MAX_PHONE_NUMBER_SIZE)

typedef struct Type {
    char name[MAX_NAME_SIZE];
    char phoneNumber[MAX_PHONE_NUMBER_SIZE];
} Type;