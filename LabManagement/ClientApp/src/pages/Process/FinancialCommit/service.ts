import request from 'umi-request';
import { ISolveFormParam } from '@/models/entity';

export async function queryFinancialDetail(formid: number) {
    return request('/api/form/financial', {
        method: 'GET',
        params: {
            formid: formid
        }
    });
}

export async function approveFinancial(param: ISolveFormParam) {
    return request('/api/form/financial/approve', {
        method: 'POST',
        data: param
    });
}

export async function rejectFinancial(param: ISolveFormParam) {
    return request('/api/form/financial/reject', {
        method: 'POST',
        data: param
    });
}