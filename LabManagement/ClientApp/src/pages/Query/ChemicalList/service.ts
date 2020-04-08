import request from 'umi-request';
import { IChemical } from '@/models/entity';

interface ParamsType extends Partial<IChemical> {
  count?: number;
}

export async function queryChemicals() {
  return request('/api/chemicals', {
    method: 'GET'
  });
}

export async function removeFakeList(params: ParamsType) {
  const { count = 5, ...restParams } = params;
  return request('/api/fake_chemicals', {
    method: 'POST',
    params: {
      count,
    },
    data: {
      ...restParams,
      method: 'delete',
    },
  });
}

export async function addFakeList(params: ParamsType) {
  const { count = 5, ...restParams } = params;
  return request('/api/fake_chemicals', {
    method: 'POST',
    params: {
      count,
    },
    data: {
      ...restParams,
      method: 'post',
    },
  });
}

export async function updateFakeList(params: ParamsType) {
  const { count = 5, ...restParams } = params;
  return request('/api/fake_chemicals', {
    method: 'POST',
    params: {
      count,
    },
    data: {
      ...restParams,
      method: 'update',
    },
  });
}
