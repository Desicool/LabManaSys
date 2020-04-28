import {
  Card,
  Table,
} from 'antd';
import { GridContent, PageHeaderWrapper } from '@ant-design/pro-layout';
import React, { FC, useEffect } from 'react';
import { connect, Dispatch, MyChemicalModelState } from 'umi';
import styles from './style.less';
import { IChemical } from '@/models/entity';

interface claimProcessProps {
  chemicals: IChemical[];
  dispatch: Dispatch;
}

const myChemicalComponent: FC<claimProcessProps> = (props) => {
  const { dispatch, chemicals } = props;
  useEffect(() => {
    dispatch({
      type: 'myChemical/fetch',
    });
  }, []);
  const columns = [
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
      title: '预计归还时间',
      dataIndex: 'rtime'
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
  }: {
    myChemical: MyChemicalModelState;
  }) => ({
    chemicals: myChemical.data,
  }),
)(myChemicalComponent);
