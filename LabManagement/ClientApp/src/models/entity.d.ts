export interface IChemical {
    id?: number;
    name?: string;
    labId?: number;
    wfId?: number;
    labName?: string;
    amount?: number;
    fname?: string;
    // or Date type?
    ptime?: string;
    // 单价
    unitprice?: number;
    // 计量单位
    unitmeasurement?: string;
    state?: string;
}

export interface IWorkFlow {
    id?: number;
    uid?: number;
    chemicals?: IChemical[];
    startTime?: string;
    state?: string;
    // user?: IUser;
}

export interface IFinancialForm {
    id?: number;
    // workflow id
    wid?: number;
    // lab id
    lid?: number;
    // 申请人
    uid?: number;
    price?: number;
    // 收款方
    receiver?: string;
    // 处理人id
    hid?: number;
    // 提交时间
    stime?: string;
    state?: string;
}

export interface IDeclarationForm {
    id?: number;
    // workflow id
    wid?: number;
    // lab id
    lid?: number;
    // 申请人
    uid?: number;
    reason?: string;
    // 处理人id
    hid?: number;
    // 提交时间
    stime?: string;
    state?: string;
}

export interface IClaimForm{
    id?:number;
    lid?:number;
    // 申请人
    uid?: number;
    // 预计归还时间
    rtime?: string;
    // 实际归还时间
    rrtime?: string;
    // 审核人
    hid?: number;
    // 提交时间
    stime?: number;
    state?: string;
}