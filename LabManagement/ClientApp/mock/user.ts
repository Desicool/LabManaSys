import { Request, Response } from 'express';
import { IUser, IRole } from '@/models/user';

const mockuser: IUser = {
  userId: 1,
  userName: 'test',
  userPassword: '123456',
  labId: 1,
  labName: 'testLab',
};
const mockRoles: IRole[] = [
  {
    roleId: 1,
    roleName: 'admin',
    lid: 1,
  },
  {
    roleId: 2,
    roleName: 'LabTeacher',
    lid: 3,
  }
]
function getFakeCaptcha(req: Request, res: Response) {
  return res.json('captcha-xxx');
}
// 代码中会兼容本地 service mock 以及部署站点的静态数据
export default {
  // 支持值为 Object 和 Array
  'GET /api/currentUser': mockuser,
  // GET POST 可省略
  'GET /api/users': [
    {
      key: '1',
      name: 'John Brown',
      age: 32,
      address: 'New York No. 1 Lake Park',
    },
    {
      key: '2',
      name: 'Jim Green',
      age: 42,
      address: 'London No. 1 Lake Park',
    },
    {
      key: '3',
      name: 'Joe Black',
      age: 32,
      address: 'Sidney No. 1 Lake Park',
    },
  ],
  'POST /api/account/login': (req: Request, res: Response) => {
    const { password, username, type } = req.body;
    if (password === 'admin' && username === 'admin') {
      res.send({
        success: true,
        user: mockuser,
        roles: mockRoles,
        certification: '111',
      });
      return;
    }
    if (password === 'admin' && username === 'user') {
      res.send({
        success: true,
        user: mockuser,
        roles: 
        {
          roleId: 3,
          roleName: 'Student',
          lid: 1,
        },
        certification: '111',
      });
      return;
    }
    if (type === 'mobile') {
      res.send({
        success: true,
        user: mockuser,
        roles: mockRoles,
        certification: '111',
      });
      return;
    }

    res.send({
      success: false,
    });
  },
  'POST /api/register': (req: Request, res: Response) => {
    res.send({ status: 'ok', currentAuthority: 'user' });
  },
  'GET /api/500': (req: Request, res: Response) => {
    res.status(500).send({
      timestamp: 1513932555104,
      status: 500,
      error: 'error',
      message: 'error',
      path: '/base/category/list',
    });
  },
  'GET /api/404': (req: Request, res: Response) => {
    res.status(404).send({
      timestamp: 1513932643431,
      status: 404,
      error: 'Not Found',
      message: 'No message available',
      path: '/base/category/list/2121212',
    });
  },
  'GET /api/403': (req: Request, res: Response) => {
    res.status(403).send({
      timestamp: 1513932555104,
      status: 403,
      error: 'Unauthorized',
      message: 'Unauthorized',
      path: '/base/category/list',
    });
  },
  'GET /api/401': (req: Request, res: Response) => {
    res.status(401).send({
      timestamp: 1513932555104,
      status: 401,
      error: 'Unauthorized',
      message: 'Unauthorized',
      path: '/base/category/list',
    });
  },

  'GET  /api/login/captcha': getFakeCaptcha,
};
