use std::fs;

fn main(){
    let filename = "example.txt";
    match fs::read_to_string(filename)
    {
        Ok(contents)=>{
            let lines = contents.lines();
            for (_, line) in lines.enumerate() {
                let mut first_digit_char : Option<char> = None;
                let mut last_digit_char: Option<char> = None;
                for digit in line.chars().filter(|c| c.is_digit(10)){
                    if first_digit_char == None {
                        first_digit_char = Some(digit);
                    }
                    last_digit_char = Some(digit);
                }

                match first_digit_char
                {
                    Some(digit_char)=>{
                        print!("First {}", digit_char);
                    },
                    None =>{

                        print!("None");
                    }
                }
                
                match last_digit_char
                {
                    Some(digit_char)=>{
                        println!(" Last {}", digit_char);
                    },
                    None =>{

                        println!(" None");
                    }
                }
            }
        },
        Err(error)=>{
            println!("Failed to read file: {}", error);
        }
    }
}