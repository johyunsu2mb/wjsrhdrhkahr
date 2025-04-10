#C & C++ 별찍기
#include<stdio.h>
int main(){
  int a;
  printf("개수입력:");
  scanf("%d",&a);
  for (int i=1; i<=a; i++){
    for (int ii=1;ii<=i;ii++){
      printf("*");
    }printf("\n");
  }return 0;}

#include<iostream>
int main(){
  int a;
  std::cout<<"개수입력";
  std::cin<<a;
  for (int i=1;i<=a;i++){
    for (int ii=1;ii<=i;ii++){
      std::cout<<"*";
    }std::cout<<"\n";
    }return 0;}
  
