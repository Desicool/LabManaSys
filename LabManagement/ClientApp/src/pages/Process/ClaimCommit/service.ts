import request from '../../../utils/request';
import { ISolveFormParam } from '@/models/entity';

export async function queryClaimDetail(formid: number) {
    return request('/api/form/claim', {
        method: 'GET',
        params: {
            formid: formid
        }
    });
}

export async function approveClaim(param: ISolveFormParam) {
    return request('/api/form/claim/approve', {
        method: 'POST',
        data: param
    });
}

export async function rejectClaim(param: ISolveFormParam) {
    return request('/api/form/claim/reject', {
        method: 'POST',
        data: param
    });
}