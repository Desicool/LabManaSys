import request from '../../../utils/request';
import { ISolveFormParam } from '@/models/entity';

export async function queryDeclarationDetail(formid: number) {
    return request('/api/form/declear', {
        method: 'GET',
        params: {
            formid: formid
        }
    });
}

export async function approveDeclaration(param: ISolveFormParam) {
    console.log('ok');
    return request('/api/form/declear/approve', {
        method: 'POST',
        data: param
    });
}

export async function rejectDeclaration(param: ISolveFormParam) {
    return request('/api/form/declear/reject', {
        method: 'POST',
        data: param
    });
}