import request from '@/utils/request';

export interface LoginParamsType {
  userName: string;
  password: string;
  mobile: string;
  captcha: string;
}

export interface LoginParamsForm {
  userName: string;
  password: string;
}

export async function getFakeCaptcha(mobile: string) {
  return request(`/api/login/captcha?mobile=${mobile}`);
}

export async function AccountLogin(params: LoginParamsForm) {
  const formData = new FormData();
  formData.set('username', params.userName);
  formData.set('password', params.password);
  return request('/api/account/login', {
    method: 'POST',
    data: formData,
    headers: new Headers({
      'Content-Type': 'multipart/form-data',
    }),
  });
}
