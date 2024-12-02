export interface UserForRegister {
    userName: string;
    userEmail?: string;
    password: string;
    userMobile?: number;
}

export interface UserForLogin {
    userName: string;
    password: string;
    token: string;
}