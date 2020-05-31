import {
  Card,
  Table,
} from 'antd';
import { GridContent, PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { FC, useEffect } from 'react';
import { connect, Dispatch, MyChemicalModelState, UserModelState, IUser } from 'umi';
import styles from './style.less';
import { IChemical } from '@/models/entity';
import { ColumnsType } from 'antd/lib/table';

interface claimProcessProps {
  chemicals: IChemical[];
  currentUser?: IUser;
  dispatch: Dispatch;
}

const myChemicalComponent: FC<claimProcessProps> = (props) => {
  const { dispatch, chemicals,currentUser } = props;
  useEffect(() => {
    dispatch({
      type: 'myChemical/fetch',
    });
  }, []);
  const columns:ColumnsType<IChemical> = [
    {
      title: '化学品名称',
      dataIndex: 'name',
      width: '16em'
    },
    {
      title: '化学品数量',
      dataIndex: 'amount'
    },
    {
      title: '生产厂家',
      dataIndex: 'fname',
    },
    {
      title: '操作',
      render: (_, record) => <a onClick={()=>dispatch({
        type: 'myChemical/returnChemical',
        payload: {
          uid: currentUser?.userId,
          uname: currentUser?.userName,
          
        }
      })}>归还</a>,
    }
  ]
  const dataSource = chemicals.map(u => ({
    ...u,
    key: u.id
  }));
  const pagination = {
    pageSize: 5,
    showQuickJumper: true,
    total: dataSource.length
  };
  return (
    <PageHeaderWrapper
      title="我正使用中的化学危险品列表"
    >
      <div className={styles.main}>
        <GridContent>
          <Card style={{ marginBottom: 24 }} bordered={false} title='申请的危险品'>
            <Table dataSource={dataSource} columns={columns} pagination={pagination}>
            </Table>
          </Card>
        </GridContent>
      </div>
    </PageHeaderWrapper>
  );
}

export default connect(
  ({
    myChemical,
    user
  }: {
    myChemical: MyChemicalModelState;
    user: UserModelState;
  }) => ({
    chemicals: myChemical.data,
    currentUser: user.currentUser
  }),
)(myChemicalComponent);
