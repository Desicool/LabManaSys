export interface ILoginReturn {
    success: boolean;
    user?: IUser;
    certification?: string;
}
export interface IUser {
    userId: number;
    userName: string;
    userPassword: string;
    labId: number;
    LabName: string;
}