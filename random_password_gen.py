import random
import string

def get_random_string(length):
    # choose from all lowercase letter
    letters = string.ascii_letters + ''.join([chr(x) for x in range(0x21, 0x30)])
    result_str = ''.join(random.choice(letters) for i in range(length))
    return result_str
    

if __name__ == '__main__':
    length = 0
    
    try:
        length = int(input("input password len: "))
    except:
        print("input correct integer val")
        exit(1)
    password = get_random_string(length)
    
    print(f"password: {password}")